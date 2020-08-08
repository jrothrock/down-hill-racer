using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseText;

    public float RestartDelay = 1f;
    private bool _gameHasEnded = false;
    private bool _currentlyPaused = false;

    // Start is called before the first frame update
    public void Start()
    {
        FindObjectOfType<Score>().AllowScore = true;
    }

    public void EndGame()
    {
        if (!_gameHasEnded) {
            _gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", RestartDelay);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level1");
    }

    public void HandlePause()
    {
        if (_currentlyPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        PauseText.SetActive(true);
        _currentlyPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        PauseText.SetActive(false);
        _currentlyPaused = false;
    }
}
