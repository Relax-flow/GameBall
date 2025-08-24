using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip[] musicTracks; // ������ ����������� ������
    public AudioSource audioSource; // ��������� AudioSource
    public int currentTrackIndex = 0;

    void Awake()
    {
        // ����������, ��� ��������� AudioSource ����������
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
        // ������������� �� ������� �������� �����
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ��������� ���������� ��������� ���������
        GameManager.Instance.LoadVolume();

        // ��������� ������, ���� ���� �����
        if (musicTracks != null && musicTracks.Length > 0)
        {
            PlayMusicTrack(currentTrackIndex);
        }
    }

    void OnDestroy()
    {
        // ������������ �� ������� �������� �����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���������, ��������� �� ����� �������� ����
        if (scene.name == "MainMenu") // �������� "MainMenu" �� �������� ����� ����� �������� ����
        {
            // ��������� ������, ���� ��� ��� �� ������
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
            Debug.LogError("������������ ������ �����: " + trackIndex);
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