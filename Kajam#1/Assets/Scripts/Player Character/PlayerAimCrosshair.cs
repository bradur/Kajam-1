// Date   : 14.10.2017 11:18
// Project: Kajam#1
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerAimCrosshair : MonoBehaviour {


    void Start () {
    }

    [SerializeField]
    [Range(0.05f, 2f)]
    private float rotationInterval = 1f;

    [SerializeField]
    private Vector2[] WithinAny;

    void Update () {
        float factor = KeyManager.main.GetKey(Action.AimUp) ? 1 : KeyManager.main.GetKey(Action.AimDown) ? -1 : 0;
        if (factor != 0)
        {
            Vector3 newDirection = transform.forward;
            float newZ = transform.eulerAngles.z + rotationInterval * factor;
            foreach (Vector2 within in WithinAny) {
                if (newZ > 360 || newZ < 0 || (newZ >= within.x && newZ <= within.y))
                {
                    transform.localRotation = Quaternion.Euler(0, 0, newZ);
                    break;
                }
            }

            //Debug.Log(transform.localRotation);
        }
    }
}
