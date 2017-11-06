// Date   : 06.11.2017 19:12
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool followX;
    [SerializeField]
    private bool followY;
    [SerializeField]
    private bool followZ;

    void Start () {
    
    }

    void Update () {
        Vector3 newPosition = transform.position;
        if (followX)
        {
            newPosition.x = target.position.x;
        }
        if (followY)
        {
            newPosition.y = target.position.y;
        }
        if (followZ)
        {
            newPosition.z = target.position.z;
        }
        transform.position = newPosition;
    }
}
