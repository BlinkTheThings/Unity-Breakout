using UnityEngine;

public class BallController : MonoBehaviour
{
    public delegate void BrickHitHandler();
    public event BrickHitHandler BrickHit;

    public delegate void FloorHitHandler();
    public event FloorHitHandler FloorHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            BrickHit?.Invoke();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            FloorHit?.Invoke();
        }
    }
}
