using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; set; } = 0;
    private const string ScoreKey = "HighScore";

    // ��������� �����
    public AudioMixer audioMixer; // ������ �� Audio Mixer
    public const string SoundVolumeKey = "SoundVolume"; // ���� ��� �������� ��������� ������
    public const string MusicVolumeKey = "MusicVolume"; // ���� ��� �������� ��������� ������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ���������� ����� �������
            LoadScore(); // ��������� ���������� ����
            LoadVolume(); // ��������� ����� ������������� �������� �����
        }
        else
        {
            Destroy(gameObject); // ������� ��������� ���������� ���������
        }

        // ������ �� ������ ����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ������� �� ����� �����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadVolume(); // ��������� �������� ��������� ��� ������ ����� �����
    }

    // ---------- ������ ��� �������� ����� ----------

    // ���������� ���������� (�����)
    public void SaveScore(int points)
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        string firstCompletionKey = "FirstCompletion_" + currentLevelName;

        if (!PlayerPrefs.HasKey(firstCompletionKey))
        {
            score += points;
            PlayerPrefs.SetInt(ScoreKey, score);
            PlayerPrefs.SetInt(firstCompletionKey, 1); // ���� ���������� ������ �������
            PlayerPrefs.Save();
        }
    }

    // �������� �������� ����������
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

    // ---------- ���������� ������� ����� ----------

    // ���������� ������� ���������
    public void SaveVolume()
    {
        if (audioMixer != null)
        {
            float soundVolumeDB;
            float musicVolumeDB;

            audioMixer.GetFloat("SoundVolume", out soundVolumeDB);
            audioMixer.GetFloat("MusicVolume", out musicVolumeDB);

            // ����������� �������� ������� � ���������� �����
            float soundVolumeLinear = Mathf.Pow(10, soundVolumeDB / 20);
            float musicVolumeLinear = Mathf.Pow(10, musicVolumeDB / 20);

            // ��������� � PlayerPrefs
            PlayerPrefs.SetFloat(SoundVolumeKey, soundVolumeLinear);
            PlayerPrefs.SetFloat(MusicVolumeKey, musicVolumeLinear);
            PlayerPrefs.Save();
        }
    }

    // �������� ���������� �������� ���������
    public void LoadVolume()
    {
        /*if (audioMixer == null)
        {
            Debug.LogError("Audio Mixer �� ���������� � GameManager �������!");
            return;
        }
        */
        // �������� ��������� �����
        if (PlayerPrefs.HasKey(SoundVolumeKey))
        {
            float soundVolumeLinear = PlayerPrefs.GetFloat(SoundVolumeKey);
            SetSoundVolume(soundVolumeLinear);
        }
        else
        {
            SetSoundVolume(0.5f); // ������������� ����������� �������� �� ���������
        }

        // �������� ��������� ������
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolumeLinear = PlayerPrefs.GetFloat(MusicVolumeKey);
            SetMusicVolume(musicVolumeLinear);
        }
        else
        {
            SetMusicVolume(0.5f); // ����������� �������� �� ���������
        }
    }

    // ��������� ��������� ����� (����������� �������� �������� � ��������)
    public void SetSoundVolume(float volume)
    {
        float decibelValue = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        if (audioMixer != null) 
        audioMixer.SetFloat("SoundVolume", decibelValue);
    }

    // ��������� ��������� ������ (���������� �����)
    public void SetMusicVolume(float volume)
    {
        float decibelValue = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        if (audioMixer != null)
            audioMixer.SetFloat("MusicVolume", decibelValue);
    }

    // ----- �������������� ������ -----

    // �������������� ���������� �������� �������� ��� �������� ����
    private void OnApplicationQuit()
    {
        SaveVolume(); // ��������� �������� ��������� ��� ������ �� ����
    }
}