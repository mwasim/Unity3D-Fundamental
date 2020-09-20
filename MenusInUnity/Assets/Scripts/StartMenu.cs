using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main_ViaStartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");

        Application.Quit();
    }
}
