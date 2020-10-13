using UnityEngine;

public class SpawnItem : MonoBehaviour, ISpawns
{
    public ItemPickUps_SO[] itemDefinitions;

    private int whichToSpawn = 0; //acts as index
    private int totalSpawnWeight = 0;
    private int chosen = 0; //compares itself with `whichToSpawn` to determine which item to spawn

    public Rigidbody ItemSpawned { get; set; }
    public Renderer ItemMaterial { get; set; }
    public ItemPickUp ItemType { get; set; }


    // Start is called before the first frame update
    private void Start()
    {
        foreach (var ip in itemDefinitions)
        {
            totalSpawnWeight += ip.spawnChanceWeight;
        }
    }

    public void CreateSpawn()
    {
        foreach (var ip in itemDefinitions)
        {
            whichToSpawn += ip.spawnChanceWeight;

            if (whichToSpawn >= chosen)
            {
                ItemSpawned = Instantiate(ip.itemSpawnObject, transform.position, Quaternion.identity);

                ItemMaterial = ItemSpawned.GetComponent<Renderer>();

                ItemMaterial.material = ip.itemMaterial;


                ItemType = ItemSpawned.GetComponent<ItemPickUp>();
                ItemType.itemDefinition = ip;

                break; //once found the required item, break out of the loop, otherwise it may spawn two items at once
            }
        }
    }
}
