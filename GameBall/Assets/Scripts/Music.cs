using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;
    public AudioClip[] musicTracks;
    public AudioSource audioSource;

    private int currentTrackIndex = 0;
    private bool isFading = false;
    private GameManager gameManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure AudioSource exists
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager не найден в PersistentMusic!");
        }

        // Применяем настройки громкости сразу после инициализации
        ApplyVolumeSettings();

        // Start playing music if there are tracks
        if (musicTracks != null && musicTracks.Length > 0)
        {
            PlayMusicTrack(currentTrackIndex);
        }
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
    }

    void Update()
    {
        // Automatically play the next track when the current one finishes
        if (musicTracks.Length > 1 && !audioSource.isPlaying && !isFading)
        {
            PlayNextTrack();
        }
    }

    public void PlayMusicTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            audioSource.clip = musicTracks[trackIndex];
            audioSource.Play();
            currentTrackIndex = trackIndex;
        }
        else
        {
            Debug.LogError("Invalid track index: " + trackIndex);
        }
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length; // Cycle through tracks
        PlayMusicTrack(currentTrackIndex);
    }
    private void ApplyVolumeSettings()
    {
        if (gameManager != null)
        {
            gameManager.LoadVolume();
        }
    }
}
