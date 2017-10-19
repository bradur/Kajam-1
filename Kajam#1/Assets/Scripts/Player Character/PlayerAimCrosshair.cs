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

    void Update () {
        float factor = KeyManager.main.GetKey(Action.AimUp) ? 1 : KeyManager.main.GetKey(Action.AimDown) ? -1 : 0;
        if (factor != 0)
        {
            Vector3 newDirection = transform.forward;
            transform.localRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + rotationInterval * factor);
        }
    }
}
