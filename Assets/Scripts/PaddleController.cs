using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [HideInInspector]
    public float Speed { get; set; }

    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Speed, 0.0f, 0.0f) * Time.deltaTime);
    }
}
