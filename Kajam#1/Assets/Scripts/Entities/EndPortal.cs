// Date   : 10.11.2017 22:54
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class EndPortal : MonoBehaviour {

    private Rigidbody2D playerRigidbody2D;
    private float myMass = 75;

    void Start () {
        focusCamera = Camera.main.GetComponent<FocusAndZoomInTarget>();
        playerRigidbody2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private bool isTeleporting = false;
    [SerializeField]
    [Range(1f, 10f)]
    private float zSpeed = 1f;


    private float pullSpeed = 5f;
    private float zPosition = 0f;

    void Update () {
        if (hasHit && !ProjectileManager.main.CurrentProjectileIsAlive() && KeyManager.main.GetKeyDown(Action.Teleport))
        {
            focusCamera.Init(transform);
            isTeleporting = true;
            zPosition = playerRigidbody2D.transform.position.z;
        }
        if (isTeleporting)
        {
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
            Vector2 force = transform.position - collider.gameObject.transform.position;
            force.Normalize();
            playerRigidbody2D.AddForce(force * playerRigidbody2D.mass * myMass / force.magnitude);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !hasHit)
        {
            hasHit = true;
            ProjectileManager.main.KillCurrentProjectile();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && hasHit)
        {
            hasHit = false;
        }
    }
}
