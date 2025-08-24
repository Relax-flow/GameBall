using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInfoAndPause : MonoBehaviour
{
    public Text sceneNameText; // Ссылка на текстовый элемент UI для отображения имени сцены
    public GameObject pauseMenu; // Ссылка на игровой объект с меню паузы (если нужно показывать)
    private bool isPaused = false;

    void Start()
    {
        // Устанавливаем имя текущей сцены в текстовом поле UI
        if (sceneNameText != null)
        {
            sceneNameText.text = SceneManager.GetActiveScene().name;
        }
        else
        {
            Debug.LogError("Text element for scene name is not assigned!");
        }

        // Убедимся, что меню паузы изначально скрыто
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        ResumeGame();
    }


    // Метод для переключения состояния паузы
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // Метод для приостановки игры
    private void PauseGame()
    {
        Time.timeScale = 0f; // Останавливаем время в игре (кроме UI)
        isPaused = true;

        // Активируем меню паузы (если оно есть)
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    // Метод для возобновления игры
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем время в игре
        isPaused = false;

        // Деактивируем меню паузы (если оно есть)
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
}
