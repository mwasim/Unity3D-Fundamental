using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    EASY = 1, MEDIUM = 2, HARD = 3
}

public class DifficultyLevel : MonoBehaviour
{
    private Button buttonDifficulty;
    private GameManager gameManager;

    public Difficulty difficultyLevel;

    // Start is called before the first frame update
    void Start()
    {
        buttonDifficulty = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Add event listender to click event of the button
        buttonDifficulty.onClick.AddListener(SetDifficulty);
    }    

    public void SetDifficulty()
    {
        Debug.Log("Button Clicked:" + gameObject.name);

        gameManager.StartGame(difficultyLevel);
    }
}
