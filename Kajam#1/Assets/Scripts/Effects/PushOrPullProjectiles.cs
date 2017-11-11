// Date   : 11.11.2017 15:25
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class PushOrPullProjectiles : MonoBehaviour {

    void Start () {
    
    }

    [SerializeField]
    private bool push = true;

    [SerializeField]
    [Range(1f, 200f)]
    private float mass = 75;

    [SerializeField]
    [Range(0.2f, 10f)]
    private float radius = 1f;

    void Update () {
        //foreach(Projectile projectile in ProjectileManager.main.GetNearbyProjectiles(transform.position, distance))
        foreach(Projectile projectile in ProjectileManager.main.GetCurrentProjectiles())
        {
            if (Vector2.Distance(transform.position, projectile.transform.position) <= radius)
            {
                Rigidbody2D rigidBody2D = projectile.GetComponent<Rigidbody2D>();
                Vector2 force;
                if (push)
                {
                    force = projectile.transform.position - transform.position;
                }
                else
                {
                    force = transform.position - projectile.transform.position;
                }
                force.Normalize();
                rigidBody2D.AddForce(force * rigidBody2D.mass * mass / force.magnitude);
            }
        }
    }
}
