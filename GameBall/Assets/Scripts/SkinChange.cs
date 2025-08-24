using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinChange : MonoBehaviour
{
    public SpriteRenderer skinchange;
    [SerializeField] private GameObject ball;
    public Sprite defaultSprite;
    public Sprite greenSprite;
    public Sprite redSprite;
    public Sprite whiteSprite;

    private const string SelectedSpriteKey = "SelectedSprite";
    private const string HasGreenSpriteKey = "HasGreenSprite";
    private const string HasRedSpriteKey = "HasRedSpriteKey";
    private const string HasWhiteSpriteKey = "HasWhiteSpriteKey";

    void Awake()
    {
        //��������, ��� GameData ��������������� ��� �������� ����� � �����.
        if (string.IsNullOrEmpty(GameData.SelectedSprite))
        {
            LoadSavedSprite();
        }
    }

    void Start()
    {

        // �������� SpriteRenderer �� GameObject ball
        skinchange = ball.GetComponent<SpriteRenderer>();

        // ��������, ��� SpriteRenderer ������
        if (skinchange == null)
        {
            Debug.LogError("SpriteRenderer �� ������ �� ������� ball!");
            return;
        }

        //LoadSavedSprite();  // ��������� ������ � Start()
        SetCurrentSprite(); //������������� ������, ����������� �� GameData.SelectedSpriteName

    }

    private void LoadSavedSprite()
    {
        // ��������� ��� ���������� ������� �� PlayerPrefs ��� ������ ������� ��� ���� GameData ��� �� ���������������
        GameData.SelectedSprite = PlayerPrefs.GetString(SelectedSpriteKey, "Default");

    }


    public void SetSprite(Sprite sprite)
    {
        skinchange.sprite = sprite;
    }

    public void BuyGreen()
    {
        BuySprite(12, greenSprite, "Green", HasGreenSpriteKey);
    }

    public void BuyRed()
    {
        BuySprite(32, redSprite, "Red", HasRedSpriteKey);
    }

    public void BuyWhite()
    {
        BuySprite(22, whiteSprite, "White", HasWhiteSpriteKey);
    }

    private void BuySprite(int cost, Sprite sprite, string spriteName, string hasSpriteKey)
    {
        if (GameData.Score >= cost)
        {
            GameData.Score -= cost;
            PlayerPrefs.SetInt("Score", GameData.Score); // ��������� ����������� ����
            PlayerPrefs.Save();

            // �������� ������ ��� ���������
            PlayerPrefs.SetInt(hasSpriteKey, 1); // 1 �������� true
            PlayerPrefs.Save();

            SetSprite(sprite);
            GameData.SelectedSprite = spriteName; // ����������
            PlayerPrefs.SetString(SelectedSpriteKey, spriteName);
            PlayerPrefs.Save(); // ����� ��������� ���������!

            Debug.Log("������ ������: " + spriteName);
        }
        else
        {
            Debug.Log("������������ ����� ��� ������� �������: " + spriteName);
        }
    }

    public void SetPurchasedSprite(string spriteName)
    {
        string hasSpriteKey = "";
        switch (spriteName)
        {
            case "Green":
                hasSpriteKey = HasGreenSpriteKey;
                break;
            case "Red":
                hasSpriteKey = HasRedSpriteKey;
                break;
            case "White":
                hasSpriteKey = HasWhiteSpriteKey;
                break;
            default:
                hasSpriteKey = "";
                break;
        }

        //���������, ��� �� ������ ������
        if (!string.IsNullOrEmpty(hasSpriteKey) && PlayerPrefs.GetInt(hasSpriteKey, 0) == 1) //��������� �������� PlayerPrefs, ����� ������, ��� �� �� ������
        {
            // ������ ��� ������, ������������� ���
            switch (spriteName)
            {
                case "Green":
                    SetSprite(greenSprite);
                    break;
                case "Red":
                    SetSprite(redSprite);
                    break;
                case "White":
                    SetSprite(whiteSprite);
                    break;
            }
            GameData.SelectedSprite = spriteName; // ����������
            PlayerPrefs.SetString(SelectedSpriteKey, spriteName);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("���� ������ �� ��� ������: " + spriteName);
            //���� ������ �� ��� ������, �� ������ ���������� ��������� ������ ��� ���-�� ���.
            SetSprite(defaultSprite);
            GameData.SelectedSprite = "Default"; // ����������
            PlayerPrefs.SetString(SelectedSpriteKey, "Default");
            PlayerPrefs.Save();
        }
    }

    //������������� ������, ����������� �� GameData.SelectedSpriteName
    private void SetCurrentSprite()
    {
        switch (GameData.SelectedSprite) // ����������
        {
            case "Green":
                SetSprite(greenSprite);
                break;
            case "Red":
                SetSprite(redSprite);
                break;
            case "White":
                SetSprite(whiteSprite);
                break;
            default:
                SetSprite(defaultSprite);
                break;
        }
    }
}

public static class GameData
{
    public static int Score { get; set; } = PlayerPrefs.GetInt("Score", 0); //��������� ���� �� PlayerPrefs ��� �������
    public static string SelectedSprite { get; set; } // ����������
}
