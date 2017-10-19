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

    [SerializeField]
    private Transform bottomBorder;

    private CenteredAroundPointsCamera centeredCamera;

    private void Awake()
    {
        main = this;
    }

    void Start () {
        centeredCamera = Camera.main.GetComponent<CenteredAroundPointsCamera>();
    }

    void Update () {
    
    }

    public Projectile SpawnProjectile(Vector3 startingPosition, Quaternion rotation, float speed)
    {
        Projectile newProjectile = Instantiate(projectilePrefab);
        newProjectile.transform.SetParent(container, false);
        newProjectile.Init(startingPosition, rotation, speed, centeredCamera, bottomBorder);
        return newProjectile;
    }
}
