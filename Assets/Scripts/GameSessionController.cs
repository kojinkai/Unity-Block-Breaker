using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSessionController : MonoBehaviour {

    // config params
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    //string scorePrefix;

    // state
    [SerializeField] int currentScore = 0;

    private void Awake()
    {
        // We want this gameStatusController to persist across every scene (singleton)
        // so we check if there is an existing instance and reuse that, destroying the attempted new
        // instance in the process.
        int gameStatusControllerCount = FindObjectsOfType<GameSessionController>().Length;
        if (gameStatusControllerCount > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        RenderScoreText();
    }

    // Use this for initialization
    void Update ()
    {
        Time.timeScale = gameSpeed;
	}

    void RenderScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        RenderScoreText();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled() {
        return isAutoPlayEnabled;
    }
}
