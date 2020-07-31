using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && canSpawn)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            canSpawn = false;

            Invoke(nameof(CanSpawnAgain), 1.5f);
        }
    }

    private void CanSpawnAgain()
    {
        canSpawn = true;
    }
}
