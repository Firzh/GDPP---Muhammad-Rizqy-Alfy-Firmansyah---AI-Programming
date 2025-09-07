using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenu()
    {
        Debug.Log("Go to Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
