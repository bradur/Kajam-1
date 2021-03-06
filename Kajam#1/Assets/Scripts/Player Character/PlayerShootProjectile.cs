// Date   : 15.10.2017 11:54
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerShootProjectile : MonoBehaviour {

    [SerializeField]
    private Animator shootBarAnimator;

    [SerializeField]
    private SpriteRenderer shootBarSprite;

    [SerializeField]
    private Transform shootBarMask;

    [SerializeField]
    private Transform projectilePosition;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    [Range(2f, 10f)]
    private float minSpeed = 6f;

    [SerializeField]
    [Range(11f, 30f)]
    private float maxSpeed = 15f;

    private float shootBarMaxScale = 4.2f;

    private Projectile currentProjectile = null;


    void Start () {
        shootBarAnimator.StopPlayback();
    }

    void Update () {
        if (KeyManager.main.GetKeyDown(Action.ShootArrow))
        {
            shootBarSprite.enabled = true;
            shootBarAnimator.enabled = true;
            shootBarAnimator.Play("ShootBarLoop", -1, 0f);
            SoundManager.main.PlaySound(SoundType.Charge);
        }
        if (KeyManager.main.GetKeyUp(Action.ShootArrow))
        {
            SoundManager.main.StopSound(SoundType.Charge);
            SoundManager.main.PlayRandomSound(SoundType.Shoot);
            shootBarSprite.enabled = false;
            shootBarAnimator.enabled = false;
            Shoot();
        }
        if (KeyManager.main.GetKeyUp(Action.Teleport))
        {
            if (currentProjectile != null && currentProjectile.IsActive && currentProjectile.Stationary)
            {
                SoundManager.main.PlayActionSound(Action.Teleport);
                playerTransform.transform.position = (Vector2)currentProjectile.transform.position;
                ProjectileManager.main.KillCurrentProjectile();
                currentProjectile = null;
                //currentProjectile.Die();
            }
        }
    }

    void Shoot()
    {
        float speed = Mathf.Clamp((shootBarMask.localScale.x / shootBarMaxScale) * maxSpeed, minSpeed, maxSpeed);
        currentProjectile = ProjectileManager.main.SpawnProjectile(projectilePosition.position, transform.rotation, speed);
    }
}
