using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    // Speed at which the paddle moves
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private Ball ball;
    Vector2 direction;

    // The vertical boundaries where the paddles can move
    private float minY = -3.7f;
    private float maxY = 3.7f;

    // The previous position of the ball
    private Vector2 previousBallPosition;

    // Move the paddle up or down based on the given direction
    public void Move(Vector2 direction)
    {
        float moveAmount = direction.y * moveSpeed * Time.deltaTime;
        float newYPosition = Mathf.Clamp(transform.position.y + moveAmount, minY, maxY);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    private void Start()
    {
        // Initialize the previous ball position to the current position at the start
        previousBallPosition = ball.transform.position;
    }

    void Update()
    {
        // Calculate the distance between the previous ball position and the current position
        float previousDistance = Vector2.Distance(previousBallPosition, transform.position);

        // Get the y position of the ball
        float ballYPosition = ball.transform.position.y;

        // Move the paddle only if the ball is getting closer
        if (Vector2.Distance(ball.transform.position, transform.position) < previousDistance)
        {
            direction = new Vector2(0f, ballYPosition - transform.position.y).normalized;
            Move(direction);
        }

        // Update the previous ball position for the next frame
        previousBallPosition = ball.transform.position;
    }
}