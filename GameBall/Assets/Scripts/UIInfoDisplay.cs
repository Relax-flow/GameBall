using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInfoDisplay : MonoBehaviour
{
    [Header("Scene Info")]
    public Text sceneNameText; // ������ �� ��������� ������� UI ��� ����������� ����� �����

    [Header("Timer Info")]
    public Text timerText; // ������ �� ��������� ������� UI ��� ����������� �������
    public Timer timerScript; // ������ �� ������ Timer

    void Start()
    {
        // ���������� ��� �����
        UpdateSceneName();

        // ��������� ������� ������� Timer
        if (timerScript == null)
        {
            Debug.LogError("Timer script is not assigned!  Assign the Timer script to this component.");
        }
    }

    void Update()
    {
        // ��������� ����� �������, ���� ������ Timer � ��������� ������� ���������
        UpdateTimerText();
    }

    // ����� ��� ���������� ������ � ������ �����
    private void UpdateSceneName()
    {
        if (sceneNameText != null)
        {
            sceneNameText.text = SceneManager.GetActiveScene().name;
        }
        else
        {
            Debug.LogError("Scene Name Text element is not assigned!");
        }
    }

    // ����� ��� ���������� ������ � ��������
    private void UpdateTimerText()
    {
        if (timerText != null && timerScript != null)
        {
            if (timerScript.timeLevelCondition != null)
            {
                float timeLeft = timerScript.timeLevelCondition.TimeLeft;
                int minutes = Mathf.FloorToInt(timeLeft / 60);
                int seconds = Mathf.FloorToInt(timeLeft % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                Debug.LogWarning("TimeLevelCondition is not assigned in the Timer script.");
            }
        }
        else
        {
            if (timerText == null)
            {
                Debug.LogError("Timer Text element is not assigned!");
            }
            if (timerScript == null)
            {
                Debug.LogError("Timer Script is not assigned!");
            }
        }
    }
}
