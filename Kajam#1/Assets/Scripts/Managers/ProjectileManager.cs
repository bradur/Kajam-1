// Date   : 15.10.2017 12:59
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private Transform container;
    public static ProjectileManager main;

    private void Awake()
    {
        main = this;
    }

    void Start () {
    
    }

    void Update () {
    
    }



    public void SpawnProjectile(Vector3 startingPosition, Quaternion rotation, float speed)
    {
        Projectile newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.SetParent(container, false);
        newProjectile.transform.position = startingPosition;
        newProjectile.transform.rotation = rotation;
        newProjectile.Shoot(speed);
    }
}
