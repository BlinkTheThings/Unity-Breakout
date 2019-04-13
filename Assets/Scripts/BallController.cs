using UnityEngine;

public class BallController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            gameObject.SetActive(false);
        }
    }
}
