using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Button button; // ������ �� ������ UI � ����������

    private void Start()
    {
        // ���������, ��� ������ ��������� � ����������
        if (button == null)
        {
            Debug.LogError("������ �� ��������� � ����������!");
            enabled = false; // ��������� ������, ����� �������� ������
            return;
        }
    }

    // ����� ��� �������� �� ��������� �����
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // ���� �� ��������� �� ��������� �����, ����� ��������� � ������ ����� ��� ������� ���-�� ������
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // ��������� � ������ �����
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    // ����� ��� ����������� �������� ������
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ����� ��� �������� � ���������� ����� �� �����
    public void LoadSpecificScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("��� ����� �� ����� ���� ������!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    // ����� ��� �������� � ���������� ����� �� �������
    public void LoadSpecificScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("�������� ������ �����!");
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
