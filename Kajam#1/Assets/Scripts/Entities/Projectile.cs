// Date   : 15.10.2017 13:01
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rigidBody2D;

    private float bottomBorderMinY;

    [SerializeField]
    [Range(0, 15f)]
    private float lifeTime = 10f;

    private float timeWhenInactive = 0.5f;
    private float lifeTimer = 0f;

    private bool stationary = false;
    public bool Stationary { get { return stationary; } }

    private ProjectilePool pool;

    CenteredAroundPointsCamera centeredCamera;
    private Vector3 cachedPosition;

    private SpriteRenderer sr;

    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private Color inactiveColor;

    private bool isActive = false;
    public bool IsActive { get { return isActive; } }

    public void Init(Vector3 startingPosition, Quaternion rotation, float speed, CenteredAroundPointsCamera centeredCamera, Transform bottomBorder)
    {
        lifeTimer = 0f;
        rigidBody2D.isKinematic = false;
        rigidBody2D.simulated = true;
        this.centeredCamera = centeredCamera;
        bottomBorderMinY = bottomBorder.position.y;
        transform.position = startingPosition;
        this.centeredCamera.AddPoint(transform);
        transform.rotation = rotation;
        Shoot(speed);
        sr.color = activeColor;
        isActive = true;
    }

    public void SetPool(ProjectilePool newPool)
    {
        sr = GetComponent<SpriteRenderer>();
        pool = newPool;
    }

    void Update()
    {
        if (isActive)
        {
            Vector2 velocity = rigidBody2D.velocity;
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (transform.position.y <= bottomBorderMinY)
        {
            Die();
        }
        if (!stationary && cachedPosition == transform.position)
        {
            stationary = true;
        }
        else
        {
            cachedPosition = transform.position;
        }
        if (lifeTime != 0f && stationary)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifeTime)
            {
                if (ProjectileManager.main.GetCurrentProjectile() == this)
                {
                    ProjectileManager.main.KillCurrentProjectile();
                } else
                {
                    Die();
                }
            }
        }

    }

    public void Deactivate()
    {
        centeredCamera.RemovePoint(transform);
        isActive = false;
        sr.color = inactiveColor;
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.isKinematic = true;
        rigidBody2D.simulated = false;
        if (lifeTimer < lifeTime - timeWhenInactive)
        {
            lifeTimer = lifeTime - timeWhenInactive;
        }
    }

    public void Die()
    {
        centeredCamera.RemovePoint(transform);
        //Destroy(gameObject);
        lifeTimer = 0f;
        pool.Sleep(this);
    }

    public void Shoot(float speed)
    {
        rigidBody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Die();
        }
        else if (collision.gameObject.tag == "Wall")
        {
            SoundManager.main.PlayRandomSound(SoundType.ProjectileHitWall);
        }
    }

}
