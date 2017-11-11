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

    public void Init()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerPosition.position;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
