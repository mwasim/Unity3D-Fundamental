using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public override void Init()
    {
        base.Init();

        Debug.Log($"{nameof(EnemySpawnManager)} is initialized");
    }

    public void LoadLevel()
    {
        Debug.Log("New Level is loaded");
    }
}
