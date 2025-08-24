using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    public GameObject GameObject;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.transform.position = GameObject.transform.position;
        }
    }
}
