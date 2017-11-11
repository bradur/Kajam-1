// Date   : 10.11.2017 22:54
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class EndPortal : MonoBehaviour
{

    private Rigidbody2D playerRigidbody2D;
    private float myMass = 50;

    void Start()
    {
        focusCamera = Camera.main.GetComponent<FocusAndZoomInTarget>();
        playerRigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        originalAngularDrag = playerRigidbody2D.angularDrag;
        originalGravityScale = playerRigidbody2D.gravityScale;
    }

    private bool isTeleporting = false;
    [SerializeField]
    [Range(1f, 10f)]
    private float zSpeed = 1f;


    private float pullSpeed = 5f;
    private float zPosition = 0f;
    private float originalAngularDrag;
    private float originalGravityScale;

    void Update()
    {
        if (hasHit && !isTeleporting)
        {
            if (Vector2.Distance(transform.position, playerRigidbody2D.transform.position) > 1.1f)
            {
                ResetPlayer();
            }
            else if (KeyManager.main.GetKeyDown(Action.Teleport) && !ProjectileManager.main.CurrentProjectileIsAlive())
            {

                focusCamera.Init(transform);
                isTeleporting = true;
                zPosition = playerRigidbody2D.transform.position.z;
            }
        } else if (isTeleporting) { 
            Vector3 playerPosition = Vector3.Lerp(playerRigidbody2D.transform.position, transform.position, pullSpeed * Time.unscaledDeltaTime);
            zPosition += Time.unscaledDeltaTime * zSpeed;
            playerPosition.z = zPosition;
            playerRigidbody2D.transform.position = playerPosition;
        }
    }

    private FocusAndZoomInTarget focusCamera;
    private bool hasHit = false;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float dist = Vector2.Distance(transform.position, collider.transform.position);
            float pullMass = myMass;
            if (dist < 0.5f)
            {
                playerRigidbody2D.gravityScale = 0f;
                pullMass = myMass / 5;
            }
            if (dist < 0.05f)
            {
                playerRigidbody2D.transform.position = Vector2.Lerp(playerRigidbody2D.transform.position, transform.position, Time.unscaledDeltaTime * pullSpeed);
            }
            else
            {
                Vector2 force = transform.position - collider.gameObject.transform.position;
                force.Normalize();
                playerRigidbody2D.AddForce(force * playerRigidbody2D.mass * pullMass / force.magnitude);
            }
        }
    }

    private void ResetPlayer()
    {
        Debug.Log("RESET!");
        hasHit = false;
        isTeleporting = false;
        zPosition = 0f;
        playerRigidbody2D.angularDrag = originalAngularDrag;
        playerRigidbody2D.gravityScale = originalGravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !hasHit)
        {
            hasHit = true;
            ProjectileManager.main.KillCurrentProjectile();
            playerRigidbody2D.angularDrag = 1f;
        }
    }
}
