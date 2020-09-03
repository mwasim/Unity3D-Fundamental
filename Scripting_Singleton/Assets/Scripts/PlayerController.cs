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

        StartCoroutine(UpdateScore());
    }

    private IEnumerator UpdateScore()
    {
        for (int i = 1; i <= 5; i++)
        {
            UIManager.Instance.UpdateScore(i);

            yield return new WaitForSeconds(1f);
        }
    }    
}
