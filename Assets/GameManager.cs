using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int winningScore = 3;

    public enum GameState
    {
        Serve,
        Play,
        End
    }

    public static GameManager Instance { get; private set; }
    private GameState currentState;

    [SerializeField] private Ball ball;
    [SerializeField] private SpriteRenderer Player1;
    [SerializeField] private SpriteRenderer Player2;
    [SerializeField] private SpriteRenderer wallup;
    [SerializeField] private SpriteRenderer walldown;
    [SerializeField] private SpriteRenderer rightwall;
    [SerializeField] private SpriteRenderer leftwall;

    [SerializeField] private TextMeshProUGUI Player1ScoreText;
    [SerializeField] private TextMeshProUGUI Player2ScoreText;
    [SerializeField] private TextMeshProUGUI screenText;
    private int Player1Score = 0;
    private int Player2Score = 0;


    private int servingPlayer;


    private void Awake()
    {
        // Ensure there's only one instance of the GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentState = GameState.Serve;
        servingPlayer = 1;
        ball.ResetPosition();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Serve:
                Player1ScoreText.text = Player1Score.ToString();
                Player2ScoreText.text = Player2Score.ToString();

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    float randomY = Random.Range(-1f, 1f);
                    if (servingPlayer == 1)
                    {
                        // Throw to the left random y
                        ball.LaunchBall(new Vector2(-1, randomY));
                    }
                    else
                    {
                        // Throw at random direction to the right
                        ball.LaunchBall(new Vector2(1, randomY));
                    }

                    currentState = GameState.Play;
                    screenText.gameObject.SetActive(false);
                }

                break;

            case GameState.Play:

                if (ball.IsCollidingWithPaddle(Player1))
                {
                    ball.SetBounceDirection(new Vector2(-1, 1));
                }

                if (ball.IsCollidingWithPaddle(Player2))
                {
                    ball.SetBounceDirection(new Vector2(-1, 1));
                }

                if (ball.IsCollidingWithPaddle(wallup))
                {
                    ball.SetBounceDirection(new Vector2(1, -1));
                }

                if (ball.IsCollidingWithPaddle(walldown))
                {
                    ball.SetBounceDirection(new Vector2(1, -1));
                }

                if (ball.IsCollidingWithPaddle(rightwall))
                {
                    ball.ResetPosition();
                    currentState = GameState.Serve;
                    Player1Score++;
                    servingPlayer = 1;
                    screenText.gameObject.SetActive(true);

                    // Check if Player 1 wins
                    if (Player1Score >= winningScore)
                    {
                        Player1ScoreText.text = Player1Score.ToString();
                        screenText.text = "Player 1 Wins!";
                        currentState = GameState.End;
                    }
                }

                if (ball.IsCollidingWithPaddle(leftwall))
                {
                    ball.ResetPosition();
                    currentState = GameState.Serve;
                    Player2Score++;
                    servingPlayer = 2;
                    screenText.gameObject.SetActive(true);

                    // Check if Player 2 wins
                    if (Player2Score >= winningScore)
                    {
                        Player2ScoreText.text = Player2Score.ToString();
                        screenText.text = "Player 2 Wins!";
                        currentState = GameState.End;
                    }
                }

                break;

            case GameState.End:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    screenText.text = "press Enter to play";
                    Player1Score = 0;
                    Player2Score = 0;
                    screenText.gameObject.SetActive(true);
                    currentState = GameState.Serve;
                }

                break;
        }
    }
}