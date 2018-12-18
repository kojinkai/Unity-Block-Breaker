using System;
using UnityEngine;

public class BallController : MonoBehaviour {
    // Config Params
    [SerializeField] PaddleController mainPaddle;
    [SerializeField] AudioClip[] ballCollisionSounds;
    [SerializeField] float startingVelocityX = 2f;
    [SerializeField] float startingVelocityY = 15f;

    // Add velocity to prevent the ball ricocheting
    // aimlessly off the walls
    [SerializeField] float randomPhysicsFactor = 2f;

    private Vector2 paddleToBallVector;
    private Boolean hasStarted = false;

    // Cached Component References
    AudioSource ballAudioSource;
    Rigidbody2D ballRigidbody2D;

    void Start()
    {
        paddleToBallVector = transform.position - mainPaddle.transform.position;

        // Get the audio component from our ball Game Object and cache it above
        ballAudioSource = GetComponent<AudioSource>();
        ballRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchBallOnMouseClick();
        }
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            ballRigidbody2D.velocity = new Vector2(startingVelocityX, startingVelocityY);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(mainPaddle.transform.position.x, mainPaddle.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityBoost = new Vector2(
            UnityEngine.Random.Range(0f, randomPhysicsFactor),
            UnityEngine.Random.Range(0f, randomPhysicsFactor)
        );

        if (hasStarted)
        {
            int randomClipIndex = UnityEngine.Random.Range(0, ballCollisionSounds.Length);
            AudioClip clip = ballCollisionSounds[randomClipIndex];
            ballAudioSource.PlayOneShot(clip);

            // Add some additional velocity to prevent the ball bouncing aimlessly around the scene
            ballRigidbody2D.velocity += velocityBoost;
        }
    }
}
