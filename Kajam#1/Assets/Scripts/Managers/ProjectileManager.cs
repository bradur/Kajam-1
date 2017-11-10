// Date   : 15.10.2017 12:59
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {

    [SerializeField]
    private Transform container;
    public static ProjectileManager main;

    [SerializeField]
    private ProjectilePool projectilePool;

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

    private Projectile currentProjectile;

    public void KillCurrentProjectile()
    {
        if (currentProjectile != null)
        {
            currentProjectile.Die();
            currentProjectile = null;
        }
    }

    public bool CurrentProjectileIsAlive()
    {
        return currentProjectile != null;
    }

    public Projectile SpawnProjectile(Vector3 startingPosition, Quaternion rotation, float speed)
    {
        Projectile newProjectile = projectilePool.WakeUp();
        if (newProjectile != null)
        {
            newProjectile.gameObject.SetActive(true);
            newProjectile.transform.SetParent(container, false);
            newProjectile.Init(startingPosition, rotation, speed, centeredCamera, bottomBorder);
        }
        currentProjectile = newProjectile;
        return newProjectile;
    }
}
