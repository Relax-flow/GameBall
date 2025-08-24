using UnityEngine;

public class TimeLevelCondition : MonoBehaviour
{
    [SerializeField] public float timeLimit = 4f;
    public float TimeLeft { get; private set; } 

    public float endTime;

    private void Start()
    {
        endTime = Time.time + timeLimit;
        TimeLeft = timeLimit;
    }

    void Update()
    {
        TimeLeft = Mathf.Max(0, endTime - Time.time);
    }

    public bool IsCompleted => Time.time > endTime;
}
