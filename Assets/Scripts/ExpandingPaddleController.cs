using UnityEngine;

public class ExpandingPaddleController : BlockController
{
    [SerializeField] PaddleController mainPaddle;

    private void OnCollisionEnter2D()
    {
        if (tag == "Breakable")
        {
            mainPaddle.ActivateBonusPaddle();
            HandleHit();
        }
    }
}
