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
    [Range(2f, 10f)]
    private float minSpeed = 6f;

    [SerializeField]
    [Range(11f, 30f)]
    private float maxSpeed = 15f;

    private float shootBarMaxScale = 4.2f;

    void Start () {
        shootBarAnimator.StopPlayback();
    }

    void Update () {
        if (KeyManager.main.GetKeyDown(Action.ShootArrow))
        {
            shootBarSprite.enabled = true;
            shootBarAnimator.enabled = true;
            shootBarAnimator.Play("ShootBarLoop", -1, 0f);
        }
        if (KeyManager.main.GetKeyUp(Action.ShootArrow))
        {
            shootBarSprite.enabled = false;
            shootBarAnimator.enabled = false;
            Shoot();
        }
    }

    void Shoot()
    {
        float speed = Mathf.Clamp((shootBarMask.localScale.x / shootBarMaxScale) * maxSpeed, minSpeed, maxSpeed);
        ProjectileManager.main.SpawnProjectile(projectilePosition.position, transform.rotation, speed);
    }
}
