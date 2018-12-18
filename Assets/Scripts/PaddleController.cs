using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] public float screenWidthInUnits = 16f;
    [SerializeField] public float minX = 1f;
    [SerializeField] public float maxX = 15f;

    // Cached References to Game Objects
    private GameSessionController gameSessionController;
    private BallController ball;

    void Start()
    {
        gameSessionController = FindObjectOfType<GameSessionController>();
        ball = FindObjectOfType<BallController>();
    }

    void Update()
    {
        float mousePositionX = Mathf.Clamp(GetPaddleXPos(), minX, maxX);
        Vector2 paddlePosition = new Vector2(mousePositionX, transform.position.y);
        transform.position = paddlePosition;

    }

    private float GetPaddleXPos() {
        // if we are in autoplay mode, set in GameSessionController, then track the ball position and not the mouse position
        return gameSessionController.IsAutoPlayEnabled() ? ball.transform.position.x : Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
