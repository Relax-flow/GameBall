using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip[] musicTracks; // Массив музыкальных треков
    public AudioSource audioSource; // Компонент AudioSource
    public int currentTrackIndex = 0;

    void Awake()
    {
        // Убеждаемся, что компонент AudioSource существует
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Start()
    {
        // Подписываемся на событие загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Загружаем предыдущие настройки громкости
        GameManager.Instance.LoadVolume();

        // Запускаем музыку, если есть треки
        if (musicTracks != null && musicTracks.Length > 0)
        {
            PlayMusicTrack(currentTrackIndex);
        }
    }

    void OnDestroy()
    {
        // Отписываемся от события загрузки сцены
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Проверяем, загружена ли сцена главного меню
        if (scene.name == "MainMenu") // Замените "MainMenu" на название вашей сцены главного меню
        {
            // Запускаем музыку, если она еще не играет
            if (!audioSource.isPlaying && musicTracks != null && musicTracks.Length > 0)
            {
                PlayMusicTrack(currentTrackIndex);
            }
        }
    }

    public void PlayMusicTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            audioSource.clip = musicTracks[trackIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Недопустимый индекс трека: " + trackIndex);
        }
    }

    private void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}