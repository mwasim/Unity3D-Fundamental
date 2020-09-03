using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //One way is to reference the GameManager script is below
        //However, as there's only one GameManager, we can make it Singleton
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        //The better way to access the GameManager is to use it's Singleton instance as below,
        GameManager.Instance.DisplayGameTimeElapsed();

        //Use SpawnManager's singleton to spawn item(s)
        SpawnManager.Instance.SpawnItem();

        StartCoroutine(UpdateScore());
    }

    private IEnumerator UpdateScore()
    {
        for (int i = 1; i <= 5; i++)
        {
            UIManager.Instance.UpdateScore(i * Random.Range(1, 10));

            yield return new WaitForSeconds(1f);
        }
    }    
}
