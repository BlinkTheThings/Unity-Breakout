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

    private bool gameStarted = false;
    private List<GameObject> bricks = new List<GameObject>();

    private int score;
    private int lives;
    private Vector3 ballStartPosition;

    void Start()
    {
        score = 0;
        ScoreText.text = "Score: " + score.ToString();

        lives = 3;
        LivesText.text = "Lives: " + lives.ToString();

        GameOverText.gameObject.SetActive(false);

        Ball.GetComponent<BallController>().BrickHit += new BallController.BrickHitHandler(OnBrickHit);
        Ball.GetComponent<BallController>().FloorHit += new BallController.FloorHitHandler(OnFloorHit);

        ballStartPosition = Ball.transform.position;

        Paddle.GetComponent<PaddleController>().Speed = PaddleSpeed;

        Ball.GetComponent<SpriteRenderer>().color = BaseColor * 1.5f;

        float brickWidth = Brick.GetComponent<Renderer>().bounds.size.x;
        float brickHeight = Brick.GetComponent<Renderer>().bounds.size.y;

        float left = LeftWall.GetComponent<Renderer>().bounds.max.x + (brickWidth * 0.5f);
        float top = Ceiling.GetComponent<Renderer>().bounds.min.y - (brickHeight * 1.5f);

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


    public void OnBrickHit()
    {
        score += 10;
        ScoreText.text = "Score: " + score.ToString();
    }

    public void OnFloorHit()
    {
        lives--;
        LivesText.text = "Lives: " + lives.ToString();

        Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (lives > 0)
        {
            gameStarted = false;
            Ball.transform.position = ballStartPosition;
        }
        else
        {
            GameOverText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            Ball.GetComponent<Rigidbody2D>().velocity = (Vector2.down + Vector2.right) * BallSpeed;
            gameStarted = true;
        }

        if (bricks.Count == 0)
        {
        }
    }
}
