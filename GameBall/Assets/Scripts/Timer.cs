using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] public TimeLevelCondition timeLevelCondition;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject[] winOrLose;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private TriggerForPoints m_score;
    [SerializeField] private GameObject ring;
    [SerializeField] private Transform ringTrans;
    public SceneInfoAndPause SceneInfoAndPause;
    [SerializeField] private Text moneyText;

    private float timeTaken;

    private void Update()
    {
        if (timeLevelCondition != null && timerText != null)
        {
            float timeLeft = timeLevelCondition.TimeLeft;
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (timeLevelCondition.IsCompleted)
        {
            ring.transform.position = ringTrans.position;
            LosePanel.SetActive(true);
            DisableWinOrLoseObjects();
        }

        if (TriggerForPoints.checkwin == true)
        {
            SceneInfoAndPause.TogglePause();

            // –ассчитываем врем€ прохождени€
            timeTaken = timeLevelCondition.timeLimit - timeLevelCondition.TimeLeft; // ¬рем€ прохождени€

            // —охран€ем врем€ прохождени€
            SaveTime(timeTaken);

            string currentLevelName = SceneManager.GetActiveScene().name;
            string firstCompletionKey = "FirstCompletion_" + currentLevelName;

            // ѕровер€ем, были ли очки уже начислены
            if (!PlayerPrefs.HasKey(firstCompletionKey))
            {
                GameManager.Instance.SaveScore(4);
                ShowScoreMessage("+4 очка");
                PlayerPrefs.SetInt(firstCompletionKey, 1);
                PlayerPrefs.Save();
            }
            else
            {
                HideScoreMessage();
            }

            WinPanel.SetActive(true);
            ring.transform.position = ringTrans.position;
            DisableWinOrLoseObjects();
            TriggerForPoints.checkwin = false;
        }
    }

    private void SaveTime(float time)
    {
        PlayerPrefs.SetFloat("LastTime", time);
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", time);
        }

        PlayerPrefs.Save();
    }

    private void DisableWinOrLoseObjects()
    {
        if (winOrLose != null)
        {
            foreach (GameObject obj in winOrLose)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning("ћассив winOrLose не инициализирован или пуст!");
        }
    }

    private void ShowScoreMessage(string message)
    {
        if (moneyText != null)
        {
            moneyText.gameObject.SetActive(true);
            moneyText.text = message;
        }
        else
        {
            Debug.LogWarning("Ёлемент moneyText не прикреплен к классу!");
        }
    }

    private void HideScoreMessage()
    {
        if (moneyText != null)
        {
            moneyText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Ёлемент moneyText не прикреплен к классу!");
        }
    }
}
