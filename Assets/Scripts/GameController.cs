using System.Collections.Generic;
using UnityEngine;

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

    private readonly bool gameStarted = false;
    private List<GameObject> bricks = new List<GameObject>();

    void Start()
    {
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

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            Ball.GetComponent<Rigidbody2D>().velocity = (Vector2.down + Vector2.right) * BallSpeed;
        }

        if (bricks.Count == 0)
        {
            Ball.SetActive(false);
        }
    }
}
