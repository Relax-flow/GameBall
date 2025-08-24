using UnityEngine;

public class BallSpeed : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDirection * speed, ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-rotationSpeed, rotationSpeed));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.AddForce(randomDirection * speed, ForceMode2D.Impulse);
        }
    }

}
