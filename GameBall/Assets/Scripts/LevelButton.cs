using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Tooltip("Имя сцены уровня для загрузки")]
    public string levelSceneName;
    [Tooltip("Изображение, показывающее, что уровень закрыт")]
    public GameObject LockedImage;
    [Tooltip("Изображение, показывающее, что уровень открыт")]
    public GameObject UnlockedImage;

    private Button button;
    private string levelCompletedKey;

    void Awake()
    {
        button = GetComponent<Button>();
        levelCompletedKey = "LevelCompleted_" + levelSceneName;

        if (button == null)
        {
            Debug.LogError("Кнопка не найдена на этом объекте!");
        }

        if (LockedImage == null || UnlockedImage == null)
        {
            Debug.LogError("Не все UI элементы назначены!");
        }
    }

    void Start()
    {
        // Проверяем, пройден ли предыдущий уровень (если это не первый уровень)
        bool isLevelUnlocked = IsLevelUnlocked();

        // Обновляем видимость UI и состояние кнопки
        UpdateUI(isLevelUnlocked);
    }

    // Проверяет, должен ли быть разблокирован уровень (пройден ли предыдущий)
    private bool IsLevelUnlocked()
    {
        // Первый уровень всегда разблокирован
        if (levelSceneName == SceneManager.GetSceneByBuildIndex(1).name) // Предполагаем, что первый уровень в Build Settings - это самый первый уровень.
        {
            return true;
        }

        // Находим индекс текущего уровня в Build Settings
        int currentLevelIndex = -1;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)) == levelSceneName)
            {
                currentLevelIndex = i;
                break;
            }
        }

        // Если не нашли уровень, возвращаем false
        if (currentLevelIndex == -1)
        {
            Debug.LogError("Уровень " + levelSceneName + " не найден в Build Settings!");
            return false;
        }

        // Определяем имя предыдущего уровня
        string previousLevelName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(currentLevelIndex - 1));

        // Проверяем, пройден ли предыдущий уровень
        return PlayerPrefs.GetInt("LevelCompleted_" + previousLevelName, 0) == 1;
    }

    private void UpdateUI(bool isUnlocked)
    {
        LockedImage.SetActive(!isUnlocked);
        UnlockedImage.SetActive(isUnlocked);
        button.interactable = isUnlocked;

        if (isUnlocked)
        {
            button.onClick.RemoveAllListeners(); // Очищаем предыдущие слушатели
            button.onClick.AddListener(LoadLevel);   // Подписываемся на событие нажатия кнопки
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
