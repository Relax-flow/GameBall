using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInfoDisplay : MonoBehaviour
{
    [Header("Scene Info")]
    public Text sceneNameText; // Ссылка на текстовый элемент UI для отображения имени сцены

    [Header("Timer Info")]
    public Text timerText; // Ссылка на текстовый элемент UI для отображения времени
    public Timer timerScript; // Ссылка на скрипт Timer

    void Start()
    {
        // Отображаем имя сцены
        UpdateSceneName();

        // Проверяем наличие скрипта Timer
        if (timerScript == null)
        {
            Debug.LogError("Timer script is not assigned!  Assign the Timer script to this component.");
        }
    }

    void Update()
    {
        // Обновляем текст таймера, если скрипт Timer и текстовый элемент назначены
        UpdateTimerText();
    }

    // Метод для обновления текста с именем сцены
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

    // Метод для обновления текста с таймером
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
