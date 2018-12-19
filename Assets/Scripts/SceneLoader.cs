using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    GameSessionController gameSessionController;

    void Start()
    {
        gameSessionController = FindObjectOfType<GameSessionController>();
    }
    public void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadStartScene() {
        SceneManager.LoadScene(0);
        gameSessionController.ResetGame();
    }
    public void QuitGame() {
        Application.Quit();
    }
}
