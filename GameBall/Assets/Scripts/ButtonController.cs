using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Button button; // Ссылка на кнопку UI в инспекторе

    private void Start()
    {
        // Убедитесь, что кнопка назначена в инспекторе
        if (button == null)
        {
            Debug.LogError("Кнопка не назначена в инспекторе!");
            enabled = false; // Отключаем скрипт, чтобы избежать ошибок
            return;
        }
    }

    // Метод для перехода на следующую сцену
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Если мы находимся на последней сцене, можно вернуться к первой сцене или сделать что-то другое
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Вернуться к первой сцене
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    // Метод для перезапуска текущего уровня
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Метод для перехода к конкретной сцене по имени
    public void LoadSpecificScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Имя сцены не может быть пустым!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    // Метод для перехода к конкретной сцене по индексу
    public void LoadSpecificScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Неверный индекс сцены!");
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
