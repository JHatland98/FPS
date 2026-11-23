using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the next scene in the Build Settings queue (Index 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
