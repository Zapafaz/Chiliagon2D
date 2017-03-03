using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    public static bool isPaused = false;
    public Canvas menuCanvas;
    public Button continueButton;
    public Button exitGameButton;
    public Button restartGameButton;

    void Start ()
    {
        menuCanvas = GetComponent<Canvas>();
        menuCanvas.enabled = false;
    }

    void Update ()
    {
        // If the game has started and the game isn't already paused, pause when the player presses the "Pause" button (Escape)
        if ((Input.GetButtonDown("Pause")) && (!isPaused) && (BallControl.startGame))
        {
            Pause();
        }

        // Check for clicks on the pause menu if the game is paused.
        // n.b. I think I forgot to remove these listeners after they're no longer needed. Whoops.
        if (isPaused)
        {
            continueButton.onClick.AddListener(Continue);
            exitGameButton.onClick.AddListener(ExitGame);
            restartGameButton.onClick.AddListener(RestartGame);
        }
    }

    // Pause the game and show the pause menu
    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0F;
        menuCanvas.enabled = true;
    }

    // Unpause the game and hide the pause menu
    void Continue()
    {
        isPaused = false;
        Time.timeScale = 1.0F;
        menuCanvas.enabled = false;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
