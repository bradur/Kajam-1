// Date   : 15.10.2017 13:01
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rigidBody2D;

    private float bottomBorderMinY;

    [SerializeField]
    [Range(0, 15f)]
    private float lifeTime = 10f;

    private float lifeTimer = 0f;

    void Start () {
    
    }

    CenteredAroundPointsCamera centeredCamera;

    public void Init (Vector3 startingPosition, Quaternion rotation, float speed, CenteredAroundPointsCamera centeredCamera, Transform bottomBorder)
    {
        this.centeredCamera = centeredCamera;
        bottomBorderMinY = bottomBorder.position.y;
        transform.position = startingPosition;
        this.centeredCamera.AddPoint(transform);
        transform.rotation = rotation;
        Shoot(speed);
    }

    void Update () {
        Vector2 velocity = rigidBody2D.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (transform.position.y <= bottomBorderMinY)
        {
            Die();
        }
        if (lifeTime != 0f)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer > lifeTime)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        centeredCamera.RemovePoint(transform);
        Destroy(gameObject);
    }

    public void Shoot(float speed)
    {
        rigidBody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Die();
        }
    }
}
