using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    public int currentPauseState = 0;
    public GameObject pausePanel;
    public GameObject pauseText;

    public void EndGame()
    {
        if(gameHasEnded == false){
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level1");
    }

    public void HandlePause()
    {
        if(currentPauseState == 0) {
            PauseGame();
        } else {
            ResumeGame();
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.SetActive(true);
        currentPauseState = 1;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseText.SetActive(false);
        currentPauseState = 0;
    }
}
