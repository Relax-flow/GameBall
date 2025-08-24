using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInfoAndPause : MonoBehaviour
{
    public Text sceneNameText; // ������ �� ��������� ������� UI ��� ����������� ����� �����
    public GameObject pauseMenu; // ������ �� ������� ������ � ���� ����� (���� ����� ����������)
    private bool isPaused = false;

    void Start()
    {
        // ������������� ��� ������� ����� � ��������� ���� UI
        if (sceneNameText != null)
        {
            sceneNameText.text = SceneManager.GetActiveScene().name;
        }
        else
        {
            Debug.LogError("Text element for scene name is not assigned!");
        }

        // ��������, ��� ���� ����� ���������� ������
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        ResumeGame();
    }


    // ����� ��� ������������ ��������� �����
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // ����� ��� ������������ ����
    private void PauseGame()
    {
        Time.timeScale = 0f; // ������������� ����� � ���� (����� UI)
        isPaused = true;

        // ���������� ���� ����� (���� ��� ����)
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    // ����� ��� ������������� ����
    public void ResumeGame()
    {
        Time.timeScale = 1f; // ������������ ����� � ����
        isPaused = false;

        // ������������ ���� ����� (���� ��� ����)
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
}
