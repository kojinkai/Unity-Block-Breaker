using System.Collections;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // Config params
    [SerializeField] public float screenWidthInUnits = 16f;
    [SerializeField] public float minX = 1f;
    [SerializeField] public float maxX = 15f;
    [SerializeField] public float bonusPaddleWidth = 1f;
    [SerializeField] public int bonusPaddleWidthTimer = 5;


    private bool bonusPaddleWithActive = false;

    // Cached References to Game Objects
    private GameSessionController gameSessionController;
    private BallController ball;
    private Vector3 initialScale;

    void Start()
    {
        gameSessionController = FindObjectOfType<GameSessionController>();
        ball = FindObjectOfType<BallController>();
        initialScale = transform.localScale;
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

    private void SetBonusPaddleWidthInActive() {
        bonusPaddleWithActive = false;
        transform.localScale = initialScale;
    }

    private IEnumerator SetBonusPaddleWidthActive() {
        bonusPaddleWithActive = true;
        transform.localScale += new Vector3(bonusPaddleWidth, 0, 0);
        yield return new WaitForSeconds(bonusPaddleWidthTimer);
        SetBonusPaddleWidthInActive();
    }

    public void ActivateBonusPaddle() {
        StartCoroutine(SetBonusPaddleWidthActive());
    }

}
