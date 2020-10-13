using UnityEngine;

public class SpawnItem : MonoBehaviour, ISpawns
{
    public Rigidbody ItemSpawned { get; set; }
    public Renderer ItemMaterial { get; set; }
    public ItemPickUp ItemType { get; set; }

   
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void CreateSpawn()
    {
        throw new System.NotImplementedException();
    }
}
