using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Настройки уровней")]
    public int totalLevels = 20; // Общее количество уровней
    public int levelsPerRow = 4;  // Количество уровней в одном ряду

    [Header("Префабы и спрайты")]
    public GameObject levelButtonPrefab; // Префаб кнопки уровня
    public Transform levelsContainer;   // Объект, в котором будут размещаться кнопки уровней

    public Sprite unlockedSprite;  // Спрайт для открытого уровня
    public Sprite lockedSprite;    // Спрайт для закрытого уровня
    public Sprite completedSprite; // Спрайт для пройденного уровня

    [Header("Настройка отступа")]
    public float horizontalSpacing = 100f; // Горизонтальный отступ между кнопками
    public float verticalSpacing = 150f;   // Вертикальный отступ между рядами

    [Header("Настройка масштаба спрайтов")]
    [SerializeField] private float spriteScale = 1f; // Масштаб спрайтов (1 - исходный размер)

    private int _lastUnlockedLevel = 1; // Индекс последнего открытого уровня(при старте игры)

    void Start()
    {
        _lastUnlockedLevel = PlayerPrefs.GetInt("LastUnlockedLevel", 1);
        CreateLevelButtons();
    }

    void CreateLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            int levelIndex = i; // Локальная копия для использования в лямбда-выражении

            GameObject levelButtonGO = Instantiate(levelButtonPrefab, levelsContainer);
            Button levelButton = levelButtonGO.GetComponent<Button>();
            Image buttonImage = levelButtonGO.GetComponent<Image>();
            Text buttonText = levelButtonGO.GetComponentInChildren<Text>();

            buttonText.text = levelIndex.ToString();

            // Расположение кнопки в зависимости от номера уровня
            int row = (levelIndex - 1) / levelsPerRow;
            int col = (levelIndex - 1) % levelsPerRow;
            RectTransform buttonRect = levelButtonGO.GetComponent<RectTransform>();

            buttonRect.anchoredPosition = new Vector2(col * horizontalSpacing, -row * verticalSpacing);

            // Установка спрайта и функциональности кнопки
            if (levelIndex <= _lastUnlockedLevel)
            {
                // Открытый или пройденный уровень
                if (IsLevelCompleted(levelIndex))
                {
                    buttonImage.sprite = completedSprite;
                }
                else
                {
                    buttonImage.sprite = unlockedSprite;
                }

                levelButton.onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                // Закрытый уровень
                buttonImage.sprite = lockedSprite;
                levelButton.interactable = false; // Делаем кнопку неактивной
            }

            // Применение масштаба к спрайту
            buttonImage.transform.localScale = Vector3.one * spriteScale;
        }
    }

    // Загрузка уровня
    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("LvL " + levelIndex); // Или как у вас называются сцены уровней
    }

    // Проверка, пройден ли уровень (заглушка, замените своей логикой)
    bool IsLevelCompleted(int levelIndex)
    {
        //  Пример: используем PlayerPrefs для хранения информации о пройденных уровнях
        return PlayerPrefs.GetInt("LevelCompleted_" + levelIndex, 0) == 1;
    }

    // (Этот метод нужно вызывать после прохождения уровня)
    public void MarkLevelCompleted(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelCompleted_" + levelIndex, 1);

        if (levelIndex == _lastUnlockedLevel && _lastUnlockedLevel < totalLevels)
        {
            _lastUnlockedLevel++;
            PlayerPrefs.SetInt("LastUnlockedLevel", _lastUnlockedLevel);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // перезагрузка сцены
    }
    public void UpdateLevels()
    {
        if (TriggerForPoints.money > 4 && _lastUnlockedLevel < totalLevels)
        {
            _lastUnlockedLevel++;
            PlayerPrefs.SetInt("LastUnlockedLevel", _lastUnlockedLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // перезагрузка сцены
        }
    }
}

