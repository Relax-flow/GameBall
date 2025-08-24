using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; set; } = 0;
    private const string ScoreKey = "HighScore";

    // Настройки звука
    public AudioMixer audioMixer; // Ссылка на Audio Mixer
    public const string SoundVolumeKey = "SoundVolume"; // Ключ для хранения громкости звуков
    public const string MusicVolumeKey = "MusicVolume"; // Ключ для хранения громкости музыки

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Оставляем объект постоянным между сценами
            LoadScore(); // Загружаем предыдущий счёт
            LoadVolume(); // Загружаем ранее установленные значения звука
        }
        else
        {
            Destroy(gameObject); // Удаляем повторные экземпляры менеджера
        }

        // Следим за сменой сцен
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Реакция на смену сцены
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadVolume(); // Применяем звуковые настройки при каждой смене сцены
    }

    // ---------- Методы для подсчёта очков ----------

    // Сохранение результата (очков)
    public void SaveScore(int points)
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        string firstCompletionKey = "FirstCompletion_" + currentLevelName;

        if (!PlayerPrefs.HasKey(firstCompletionKey))
        {
            score += points;
            PlayerPrefs.SetInt(ScoreKey, score);
            PlayerPrefs.SetInt(firstCompletionKey, 1); // Флаг завершения уровня впервые
            PlayerPrefs.Save();
        }
    }

    // Загрузка текущего результата
    private void LoadScore()
    {
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            score = PlayerPrefs.GetInt(ScoreKey);
        }
        else
        {
            score = 0;
        }
    }

    // ---------- Управление уровнем звука ----------

    // Сохранение уровней громкости
    public void SaveVolume()
    {
        if (audioMixer != null)
        {
            float soundVolumeDB;
            float musicVolumeDB;

            audioMixer.GetFloat("SoundVolume", out soundVolumeDB);
            audioMixer.GetFloat("MusicVolume", out musicVolumeDB);

            // Преобразуем децибелы обратно в нормальную форму
            float soundVolumeLinear = Mathf.Pow(10, soundVolumeDB / 20);
            float musicVolumeLinear = Mathf.Pow(10, musicVolumeDB / 20);

            // Сохраняем в PlayerPrefs
            PlayerPrefs.SetFloat(SoundVolumeKey, soundVolumeLinear);
            PlayerPrefs.SetFloat(MusicVolumeKey, musicVolumeLinear);
            PlayerPrefs.Save();
        }
    }

    // Загрузка предыдущих настроек громкости
    public void LoadVolume()
    {
        /*if (audioMixer == null)
        {
            Debug.LogError("Audio Mixer не установлен в GameManager скрипте!");
            return;
        }
        */
        // Загрузка громкости звука
        if (PlayerPrefs.HasKey(SoundVolumeKey))
        {
            float soundVolumeLinear = PlayerPrefs.GetFloat(SoundVolumeKey);
            SetSoundVolume(soundVolumeLinear);
        }
        else
        {
            SetSoundVolume(0.5f); // Устанавливаем стандартное значение по умолчанию
        }

        // Загрузка громкости музыки
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolumeLinear = PlayerPrefs.GetFloat(MusicVolumeKey);
            SetMusicVolume(musicVolumeLinear);
        }
        else
        {
            SetMusicVolume(0.5f); // Стандартное значение по умолчанию
        }
    }

    // Установка громкости звука (преобразует линейное значение в децибелы)
    public void SetSoundVolume(float volume)
    {
        float decibelValue = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        if (audioMixer != null) 
        audioMixer.SetFloat("SoundVolume", decibelValue);
    }

    // Установка громкости музыки (аналогично звуку)
    public void SetMusicVolume(float volume)
    {
        float decibelValue = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        if (audioMixer != null)
            audioMixer.SetFloat("MusicVolume", decibelValue);
    }

    // ----- Дополнительные методы -----

    // Автоматическое сохранение звуковых настроек при закрытии игры
    private void OnApplicationQuit()
    {
        SaveVolume(); // Сохраняем звуковые настройки при выходе из игры
    }
}