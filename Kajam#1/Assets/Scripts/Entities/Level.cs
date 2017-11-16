// Date   : 11.11.2017 10:00
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{

    [SerializeField]
    private Transform playerPosition;

    private float originalGravity = 5f;

    [SerializeField]
    private bool startMusic = false;

    [SerializeField]
    private bool theEnd = false;

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
        if (startMusic)
        {
            SoundManager.main.StartMusic();
        }
        if (theEnd)
        {
            LevelManager.main.SetLevelStart();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        float originalZ = player.transform.position.z;
        Vector3 newPosition = playerPosition.position;
        newPosition.z = originalZ;
        player.transform.position = newPosition;
        player.GetComponent<Rigidbody2D>().gravityScale = originalGravity;
    }

    /*public void Kill()
    {
        gameObject.SetActive(false);
    }*/
}
