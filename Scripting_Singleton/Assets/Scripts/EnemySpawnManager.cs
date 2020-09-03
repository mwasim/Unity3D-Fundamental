using UnityEngine;

/*
    EnemySpawnManager also works just like other Singletons (e.g. GameManager, UIManager etc.)
    However, it's derived from the base MonoSingleton to reuse the code instead of duplicating the simialar code repeatedly
 */
public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    public override void Init()
    {
        base.Init();

        Debug.Log($"{nameof(EnemySpawnManager)} is initialized");
    }

    public void SpawnEnemy()
    {
        Debug.Log("Enemy is spawned");
    }
}
