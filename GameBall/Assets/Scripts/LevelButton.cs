using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [Tooltip("��� ����� ������ ��� ��������")]
    public string levelSceneName;
    [Tooltip("�����������, ������������, ��� ������� ������")]
    public GameObject LockedImage;
    [Tooltip("�����������, ������������, ��� ������� ������")]
    public GameObject UnlockedImage;

    private Button button;
    private string levelCompletedKey;

    void Awake()
    {
        button = GetComponent<Button>();
        levelCompletedKey = "LevelCompleted_" + levelSceneName;

        if (button == null)
        {
            Debug.LogError("������ �� ������� �� ���� �������!");
        }

        if (LockedImage == null || UnlockedImage == null)
        {
            Debug.LogError("�� ��� UI �������� ���������!");
        }
    }

    void Start()
    {
        // ���������, ������� �� ���������� ������� (���� ��� �� ������ �������)
        bool isLevelUnlocked = IsLevelUnlocked();

        // ��������� ��������� UI � ��������� ������
        UpdateUI(isLevelUnlocked);
    }

    // ���������, ������ �� ���� ������������� ������� (������� �� ����������)
    private bool IsLevelUnlocked()
    {
        // ������ ������� ������ �������������
        if (levelSceneName == SceneManager.GetSceneByBuildIndex(1).name) // ������������, ��� ������ ������� � Build Settings - ��� ����� ������ �������.
        {
            return true;
        }

        // ������� ������ �������� ������ � Build Settings
        int currentLevelIndex = -1;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)) == levelSceneName)
            {
                currentLevelIndex = i;
                break;
            }
        }

        // ���� �� ����� �������, ���������� false
        if (currentLevelIndex == -1)
        {
            Debug.LogError("������� " + levelSceneName + " �� ������ � Build Settings!");
            return false;
        }

        // ���������� ��� ����������� ������
        string previousLevelName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(currentLevelIndex - 1));

        // ���������, ������� �� ���������� �������
        return PlayerPrefs.GetInt("LevelCompleted_" + previousLevelName, 0) == 1;
    }

    private void UpdateUI(bool isUnlocked)
    {
        LockedImage.SetActive(!isUnlocked);
        UnlockedImage.SetActive(isUnlocked);
        button.interactable = isUnlocked;

        if (isUnlocked)
        {
            button.onClick.RemoveAllListeners(); // ������� ���������� ���������
            button.onClick.AddListener(LoadLevel);   // ������������� �� ������� ������� ������
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
