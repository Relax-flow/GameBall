using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("��������� �������")]
    public int totalLevels = 20; // ����� ���������� �������
    public int levelsPerRow = 4;  // ���������� ������� � ����� ����

    [Header("������� � �������")]
    public GameObject levelButtonPrefab; // ������ ������ ������
    public Transform levelsContainer;   // ������, � ������� ����� ����������� ������ �������

    public Sprite unlockedSprite;  // ������ ��� ��������� ������
    public Sprite lockedSprite;    // ������ ��� ��������� ������
    public Sprite completedSprite; // ������ ��� ����������� ������

    [Header("��������� �������")]
    public float horizontalSpacing = 100f; // �������������� ������ ����� ��������
    public float verticalSpacing = 150f;   // ������������ ������ ����� ������

    [Header("��������� �������� ��������")]
    [SerializeField] private float spriteScale = 1f; // ������� �������� (1 - �������� ������)

    private int _lastUnlockedLevel = 1; // ������ ���������� ��������� ������(��� ������ ����)

    void Start()
    {
        _lastUnlockedLevel = PlayerPrefs.GetInt("LastUnlockedLevel", 1);
        CreateLevelButtons();
    }

    void CreateLevelButtons()
    {
        for (int i = 1; i <= totalLevels; i++)
        {
            int levelIndex = i; // ��������� ����� ��� ������������� � ������-���������

            GameObject levelButtonGO = Instantiate(levelButtonPrefab, levelsContainer);
            Button levelButton = levelButtonGO.GetComponent<Button>();
            Image buttonImage = levelButtonGO.GetComponent<Image>();
            Text buttonText = levelButtonGO.GetComponentInChildren<Text>();

            buttonText.text = levelIndex.ToString();

            // ������������ ������ � ����������� �� ������ ������
            int row = (levelIndex - 1) / levelsPerRow;
            int col = (levelIndex - 1) % levelsPerRow;
            RectTransform buttonRect = levelButtonGO.GetComponent<RectTransform>();

            buttonRect.anchoredPosition = new Vector2(col * horizontalSpacing, -row * verticalSpacing);

            // ��������� ������� � ���������������� ������
            if (levelIndex <= _lastUnlockedLevel)
            {
                // �������� ��� ���������� �������
                if (IsLevelCompleted(levelIndex))
                {
                    buttonImage.sprite = completedSprite;
                }
                else
                {
                    buttonImage.sprite = unlockedSprite;
                }

                levelButton.onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                // �������� �������
                buttonImage.sprite = lockedSprite;
                levelButton.interactable = false; // ������ ������ ����������
            }

            // ���������� �������� � �������
            buttonImage.transform.localScale = Vector3.one * spriteScale;
        }
    }

    // �������� ������
    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("LvL " + levelIndex); // ��� ��� � ��� ���������� ����� �������
    }

    // ��������, ������� �� ������� (��������, �������� ����� �������)
    bool IsLevelCompleted(int levelIndex)
    {
        //  ������: ���������� PlayerPrefs ��� �������� ���������� � ���������� �������
        return PlayerPrefs.GetInt("LevelCompleted_" + levelIndex, 0) == 1;
    }

    // (���� ����� ����� �������� ����� ����������� ������)
    public void MarkLevelCompleted(int levelIndex)
    {
        PlayerPrefs.SetInt("LevelCompleted_" + levelIndex, 1);

        if (levelIndex == _lastUnlockedLevel && _lastUnlockedLevel < totalLevels)
        {
            _lastUnlockedLevel++;
            PlayerPrefs.SetInt("LastUnlockedLevel", _lastUnlockedLevel);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������ �����
    }
    public void UpdateLevels()
    {
        if (TriggerForPoints.money > 4 && _lastUnlockedLevel < totalLevels)
        {
            _lastUnlockedLevel++;
            PlayerPrefs.SetInt("LastUnlockedLevel", _lastUnlockedLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������ �����
        }
    }
}

