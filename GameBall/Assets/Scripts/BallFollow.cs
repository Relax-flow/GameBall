using UnityEngine;

public class BallFollow : MonoBehaviour
{
    public GameObject ball;
    public float speedThreshold = 1.0f; 
    private TrailRenderer trailRenderer;

    private void Start()
    {
        if (ball != null)
        {
            trailRenderer = ball.GetComponentInChildren<TrailRenderer>();

                trailRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (ball != null)
        {
            transform.position = ball.transform.position;

            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
            if (ballRigidbody != null)
            {
                float speed = ballRigidbody.velocity.magnitude;

                if (trailRenderer != null)
                {
                    trailRenderer.enabled = speed > speedThreshold;
                }
            }
        }
    }
}