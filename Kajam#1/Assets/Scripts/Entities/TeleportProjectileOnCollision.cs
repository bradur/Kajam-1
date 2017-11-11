// Date   : 06.11.2017 22:22
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class TeleportProjectileOnCollision : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<Projectile>() != null)
        {
            collision2D.gameObject.transform.position = target.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.GetComponent<Projectile>() != null)
        {
            collider2D.gameObject.transform.position = target.position;
        }
    }
}
