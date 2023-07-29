using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Speed at which the paddle moves
    [SerializeField] private float moveSpeed = 6f;

    // The vertical boundaries where the paddles can move
    private float minY = -3.7f;
    private float maxY = 3.7f;

    // Move the paddle up or down based on the given direction
    public void Move(Vector2 direction)
    {
        float moveAmount = direction.y * moveSpeed * Time.deltaTime;
        float newYPosition = Mathf.Clamp(transform.position.y + moveAmount, minY, maxY);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector2.up);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector2.down);
        }
    }
}