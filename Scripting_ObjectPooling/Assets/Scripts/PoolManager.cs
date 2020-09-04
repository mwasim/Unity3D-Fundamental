using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager> //Step-1: Turn this class into a Singleton
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private GameObject _projectilesContainer;

    [SerializeField]
    private int _prePopulatedProjectileCount;

    public List<GameObject> ProjectilePrefabListPool { get; private set; } = new List<GameObject>();

    private Vector3 _projectileInitialPosition;

    public override void Init()
    {
        base.Init();

        _projectileInitialPosition = _projectilePrefab.transform.position;

        for (int i = 0; i < _prePopulatedProjectileCount; i++)
        {
            AddNewProjectileToThePool();
        }
    }

    private GameObject AddNewProjectileToThePool()
    {
        //var projectile = Instantiate(_projectilePrefab, _projectilePrefab.transform.position, Quaternion.identity, _projectilesContainer.transform);
        var projectile = Instantiate(_projectilePrefab);

        projectile.transform.parent = _projectilesContainer.transform;

        projectile.SetActive(false); //by default the projectile is not active
        //projectile.AddComponent<Projectile>(); //Add Projectile script component - Script component was already added in the inspector so no need to add here

        ProjectilePrefabListPool.Add(projectile);

        return projectile;
    }

    public GameObject AvailableProjectile
    {
        get
        {
            foreach (var item in ProjectilePrefabListPool)
            {
                if (!item.activeInHierarchy) //projectiles being used are set to active and to reuse we need a projectile which is inactive
                {
                    item.SetActive(true); //ensure to make the found projectile active
                    item.transform.position = _projectileInitialPosition; //ensure the projectile is on the starting position on returning

                    return item;
                }
            }

            //If all available projectiles are being used, then add new projectile and return it
            var projectile = AddNewProjectileToThePool();
            projectile.SetActive(true);
            projectile.transform.position = _projectileInitialPosition;

            return projectile;
        }
    }
}
