using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Brick;
    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject Ceiling;
    public GameObject Floor;
    public GameObject Ball;
    public GameObject Paddle;
    public Color BaseColor;
    public float BallSpeed;
    public float PaddleSpeed;
    public Text ScoreText; 
    public Text LivesText;
    public Text GameOverText;
    public Text WinText;

    enum GameState { NewGame, Playing, NewBall, WinGame, GameOver};

    private GameState gameState;

    private List<GameObject> bricks;

    private int score;
    private int lives;
    private Vector3 ballStartPosition;

    void Start()
    {
        Ball.GetComponent<BallController>().BrickHit += new BallController.BrickHitHandler(OnBrickHit);
        Ball.GetComponent<BallController>().FloorHit += new BallController.FloorHitHandler(OnFloorHit);
        Ball.GetComponent<SpriteRenderer>().color = BaseColor * 1.5f;

        ballStartPosition = Ball.transform.position;

        Paddle.GetComponent<PaddleController>().Speed = PaddleSpeed;

        NewGame();
    }

    private void NewGame()
    {
        Ball.transform.position = ballStartPosition;

        gameState = GameState.NewGame;

        score = 0;
        ScoreText.text = "SCORE: " + score.ToString();

        lives = 3;
        LivesText.text = "LIVES: " + lives.ToString();

        GameOverText.gameObject.SetActive(false);
        WinText.gameObject.SetActive(false);

        float brickWidth = Brick.GetComponent<Renderer>().bounds.size.x;
        float brickHeight = Brick.GetComponent<Renderer>().bounds.size.y;

        float left = LeftWall.GetComponent<Renderer>().bounds.max.x + (brickWidth * 0.5f);
        float top = Ceiling.GetComponent<Renderer>().bounds.min.y - (brickHeight * 1.5f);

        if (bricks != null)
        {
            foreach(var brick in bricks)
            {
                Destroy(brick);
            }
        }

        bricks = new List<GameObject>();

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var newBrick = Instantiate(Brick);
                newBrick.transform.position = new Vector3(left + (col * brickWidth), top - (row * brickHeight));
                newBrick.GetComponent<SpriteRenderer>().color = BaseColor * (1 - (row * 0.2f));
                bricks.Add(newBrick);
            }
        }
    }

    public void OnBrickHit(GameObject brick)
    {
        score += 10;
        ScoreText.text = "SCORE: " + score.ToString();

        bricks.Remove(brick);
        Destroy(brick);

        if (bricks.Count == 0)
        {
            Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameState = GameState.WinGame;
            WinText.gameObject.SetActive(true);
        }
    }

    public void OnFloorHit()
    {
        lives--;
        LivesText.text = "LIVES: " + lives.ToString();

        Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (lives > 0)
        {
            gameState = GameState.NewBall;
            Ball.transform.position = ballStartPosition;
        }
        else
        {
            gameState = GameState.GameOver;
            GameOverText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.NewGame || gameState == GameState.NewBall)
            {
                gameState = GameState.Playing;
                Ball.GetComponent<Rigidbody2D>().velocity = (Vector2.down + Vector2.right) * BallSpeed;
            }
            else if (gameState == GameState.GameOver || gameState == GameState.WinGame)
            {
                NewGame();
            }
        }
    }
}
