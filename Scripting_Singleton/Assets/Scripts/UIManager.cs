using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Singleton (Other classes can access this single instance)
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }

            return _instance;
        }
    }

    //To intialize the Singleton we use the Awake() method
    private void Awake()
    {
        _instance = this;
    }

    public void UpdateScore(int score)
    {
        Debug.Log("Score: " + score);

        Debug.Log("UIManager is notifying the updated score to the GameManager...");
        GameManager.Instance.DisplayGameTimeElapsed();
    }
}
