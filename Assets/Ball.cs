using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Speed at which the ball moves
    public float moveSpeed = 10f;

    // Direction of the ball's movement
    private Vector2 direction;

    // Reset the ball's position to the center of the screen
    public void ResetPosition()
    {
        transform.position = Vector2.zero;
        direction = Vector2.zero;
    }

    // Launch the ball in a specified direction
    public void LaunchBall(Vector2 launchDirection)
    {
        direction = launchDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the ball based on its direction and moveSpeed
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }


    public bool IsCollidingWithPaddle(SpriteRenderer wall)
    {
        // Get the bounds of the ball and the paddle
        Bounds ballBounds = GetComponent<SpriteRenderer>().bounds;

        // Check if the ball's bounds intersect with the paddle's bounds
        return ballBounds.Intersects(wall.bounds);
    }

    public void SetBounceDirection(Vector2 bounce)
    {
        direction = new Vector2(direction.x * bounce.x, direction.y * bounce.y);
    }
}