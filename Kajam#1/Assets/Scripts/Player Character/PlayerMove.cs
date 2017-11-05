// Date   : 19.10.2017 23:53
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    private Rigidbody2D rigidBody2D;

    [SerializeField]
    [Range(0.2f, 50f)]
    private float speedInterval = 1f;
    [SerializeField]
    [Range(1f, 10f)]
    private float maxSpeed = 1f;
    private float velocityX = 0f;

    private Vector3 originalPosition;

    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    [SerializeField]
    private Transform bottomBorder;

    void Update () {
        if (KeyManager.main.GetKeyDown(Action.MoveLeft))
        {
            transform.right = -Vector2.right;
            //transform.forward = -Vector3.right;
        }
        bool grounded = Mathf.Abs(rigidBody2D.velocity.y) < 0.01f;
        if (KeyManager.main.GetKey(Action.MoveLeft)) {
            velocityX = rigidBody2D.velocity.x - speedInterval;
            if (Mathf.Abs(velocityX) > maxSpeed)
            {
                velocityX = -maxSpeed;
            }
            rigidBody2D.velocity = new Vector2(velocityX, rigidBody2D.velocity.y);
            Debug.Log(string.Format("{0} , {1}", velocityX, speedInterval * Time.deltaTime));
        }
        
        if (KeyManager.main.GetKeyDown(Action.MoveRight))
        {
            transform.right = Vector3.right;
        }
        if (KeyManager.main.GetKey(Action.MoveRight))
        {
            velocityX = rigidBody2D.velocity.x + speedInterval;
            if (velocityX > maxSpeed)
            {
                velocityX = maxSpeed;
            }
            rigidBody2D.velocity = new Vector2(velocityX, rigidBody2D.velocity.y);
        }
        if (transform.position.y < bottomBorder.position.y)
        {
            Die();
        }
    }

    private void Die()
    {
        //Destroy(gameObject);
        transform.position = originalPosition;
    }
}
