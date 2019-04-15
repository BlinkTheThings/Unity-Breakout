using UnityEngine;

public class BallController : MonoBehaviour
{
    public delegate void BrickHitHandler();
    public event BrickHitHandler BrickHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            BrickHit?.Invoke();

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            gameObject.SetActive(false);
        }
    }
}
