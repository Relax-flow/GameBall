using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    public AudioClip[] musicTracks; // Массив музыкальных треков
    public AudioSource audioSource; // Компонент AudioSource
    public int currentTrackIndex = 0;

    void Awake()
    {
        // Создаем постоянный экземпляр компонента
        if (FindObjectsOfType<PersistentAudio>().Length > 1)
        {
            Destroy(this.gameObject); // Если экземпляров больше одного, удаляем себя
            return;
        }

        // Этот объект останется постоянным между всеми сценами
        DontDestroyOnLoad(this.gameObject);

        // Ищем и присваиваем компонент AudioSource
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
        // Начинаем воспроизведение, если есть музыкальные треки
        if (musicTracks != null && musicTracks.Length > 0)
        {
            PlayMusicTrack(currentTrackIndex);
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
            Debug.LogError("Неверный индекс трека: " + trackIndex);
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