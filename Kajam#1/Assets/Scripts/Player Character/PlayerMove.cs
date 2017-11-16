// Date   : 19.10.2017 23:53
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D rigidBody2D;

    [SerializeField]
    [Range(0.2f, 50f)]
    private float speedInterval = 1f;
    [SerializeField]
    [Range(1f, 10f)]
    private float maxSpeed = 1f;
    private float velocityX = 0f;

    /*
    [Range(1f, 10f)]
    [SerializeField]
    private float forceSpeed;*/

    private Vector3 originalPosition;

    [SerializeField]
    private Transform playerAimer;

    [SerializeField]
    private PlayerShootProjectile playerShoot;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        originalZPosition = originalPosition.z;
    }

    [SerializeField]
    private Transform bottomBorder;

    private bool allowVerticalMovement = false;
    private bool comingOutOfTeleport = false;
    private float originalZPosition;
    private float zSpeed = 5f;
    private float zPosition;
    private float pullSpeed = 25f;
    private Vector3 targetPosition;
    void Update()
    {
        if (comingOutOfTeleport)
        {
            zPosition -= Time.unscaledDeltaTime * zSpeed;
            if (zPosition <= originalZPosition)
            {
                zPosition = originalZPosition;
                comingOutOfTeleport = false;
                EnableShooting();
            }
            targetPosition = transform.position;
            targetPosition.z = zPosition;
            transform.position = targetPosition;
        }
        else
        {
            if (KeyManager.main.GetKeyDown(Action.MoveLeft))
            {
                //transform.right = -Vector2.right;
                playerAimer.right = -Vector2.right;
                //transform.forward = -Vector3.right;
            }
            else if (KeyManager.main.GetKeyDown(Action.MoveRight))
            {
                //transform.right = Vector2.right;
                playerAimer.right = Vector2.right;
            }

            if (KeyManager.main.GetKey(Action.MoveLeft))
            {
                if (playerAimer.right.x > 0f)
                {
                    playerAimer.right = -Vector2.right;
                }
                velocityX = rigidBody2D.velocity.x - speedInterval;
                if (Mathf.Abs(velocityX) > maxSpeed)
                {
                    velocityX = -maxSpeed;
                }
                rigidBody2D.velocity = new Vector2(velocityX, rigidBody2D.velocity.y);
                //rigidBody2D.AddForce(new Vector2(-forceSpeed, 0f), ForceMode2D.Force);
            }
            else if (KeyManager.main.GetKey(Action.MoveRight))
            {
                if (playerAimer.right.x < 0f)
                {
                    playerAimer.right = Vector2.right;
                }
                velocityX = rigidBody2D.velocity.x + speedInterval;
                if (velocityX > maxSpeed)
                {
                    velocityX = maxSpeed;
                }
                rigidBody2D.velocity = new Vector2(velocityX, rigidBody2D.velocity.y);
                //rigidBody2D.AddForce(new Vector2(forceSpeed, 0f), ForceMode2D.Force);
            }
            if (allowVerticalMovement)
            {
                float velocityY;
                if (KeyManager.main.GetKey(Action.MoveUp))
                {
                    velocityY = rigidBody2D.velocity.y + speedInterval;
                    if (velocityY > maxSpeed)
                    {
                        velocityY = maxSpeed;
                    }
                    rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, velocityY);
                    //rigidBody2D.AddForce(new Vector2(forceSpeed, 0f), ForceMode2D.Force);
                }
                else if (KeyManager.main.GetKey(Action.MoveDown))
                {
                    velocityY = rigidBody2D.velocity.y - speedInterval;
                    if (velocityY > maxSpeed)
                    {
                        velocityY = maxSpeed;
                    }
                    rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, velocityY);
                    //rigidBody2D.AddForce(new Vector2(forceSpeed, 0f), ForceMode2D.Force);
                }
            }
        }
        if (transform.position.y < bottomBorder.position.y)
        {
            Die();
        }
    }

    public void DisableShooting()
    {
        playerShoot.gameObject.SetActive(false);
    }

    public void EnableShooting()
    {
        playerShoot.gameObject.SetActive(true);
    }

    public void ComeOutOfTeleport()
    {
        comingOutOfTeleport = true;
        zPosition = transform.position.z;
    }

    public void StartPulling()
    {
        allowVerticalMovement = true;
    }

    public void StopPulling()
    {
        allowVerticalMovement = false;
    }

    private void Die()
    {
        //Destroy(gameObject);
        transform.position = originalPosition;
    }
}
