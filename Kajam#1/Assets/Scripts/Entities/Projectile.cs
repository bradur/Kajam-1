// Date   : 15.10.2017 13:01
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D rigidBody2D;

    void Start () {
    
    }

    void Update () {
        Vector2 velocity = rigidBody2D.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Shoot(float speed)
    {
        rigidBody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
