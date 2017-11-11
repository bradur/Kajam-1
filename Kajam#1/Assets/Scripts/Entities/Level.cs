// Date   : 11.11.2017 10:00
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{

    [SerializeField]
    private Transform playerPosition;

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Init()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float originalZ = player.transform.position.z;
        Vector3 newPosition = playerPosition.position;
        newPosition.z = originalZ;
        player.transform.position = newPosition;
    }

    /*public void Kill()
    {
        gameObject.SetActive(false);
    }*/
}
