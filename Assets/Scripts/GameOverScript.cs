using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using TMPro; // Requires TextMeshPro
public class GameOverScript : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Drag your UI Panel or Text object here")]
    public GameObject uiElement;
    public GameObject uiReticle;
    [Tooltip("How many seconds to wait before restarting")]
    public float restartDelay = 3f;

    private bool isTriggered = false;

    private void Start()
    {
        // Ensure the UI is hidden when the game starts
        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }
    }

    // Use OnTriggerEnter2D(Collider2D other) if you are making a 2D game
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the Player and prevent multiple triggers
        if (other.CompareTag("Player") && !isTriggered)
        {   
            uiReticle.SetActive(false);
            Destroy(other.gameObject);
            isTriggered = true;
            StartCoroutine(ShowUIAndRestart());
        }
    }

    private IEnumerator ShowUIAndRestart()
    {
        // 1. Show the UI
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }

        // 2. Wait for the specified number of seconds
        yield return new WaitForSeconds(restartDelay);

        // 3. Reload the currently active scene
        SceneManager.LoadScene(0);
    }
}