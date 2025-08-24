using UnityEngine;
using UnityEngine.UI;

public class StatisticsDisplay : MonoBehaviour
{
    [SerializeField] private Text lastTimeText;
    [SerializeField] private Text bestTimeText;

    void Start()
    {
        DisplayStatistics();
    }

    private void DisplayStatistics()
    {
        // Загружаем последнее время
        float lastTime = PlayerPrefs.GetFloat("LastTime", 0f);
        lastTimeText.text = FormatTime(lastTime);

        // Загружаем лучшее время
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        bestTimeText.text =(bestTime == float.MaxValue ? "N/A" : FormatTime(bestTime));
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
