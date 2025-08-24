using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    public AudioClip[] musicTracks; // ������ ����������� ������
    public AudioSource audioSource; // ��������� AudioSource
    public int currentTrackIndex = 0;

    void Awake()
    {
        // ������� ���������� ��������� ����������
        if (FindObjectsOfType<PersistentAudio>().Length > 1)
        {
            Destroy(this.gameObject); // ���� ����������� ������ ������, ������� ����
            return;
        }

        // ���� ������ ��������� ���������� ����� ����� �������
        DontDestroyOnLoad(this.gameObject);

        // ���� � ����������� ��������� AudioSource
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
        // �������� ���������������, ���� ���� ����������� �����
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
            Debug.LogError("�������� ������ �����: " + trackIndex);
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