using UnityEngine;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite defaultBallSprite;
    public Sprite redBallSprite;
    public Sprite greenBallSprite;
    public Sprite whiteBallSprite;

    [Header("Prices")]
    public int redBallPrice = 32;
    public int greenBallPrice = 12;
    public int whiteBallPrice = 22;

    [Header("Ball Prefab")]
    public GameObject ballPrefab; // ������ �� ������ ����

    private const string SelectedSpriteKey = "SelectedBallSprite";

    private bool hasRedBall = false;
    private bool hasGreenBall = false;
    private bool hasWhiteBall = false;

    [Header("UI Buttons")]
    public GameObject[] redButtons;
    public GameObject[] greenButtons;
    public GameObject[] whiteButtons;

    [Header("Checkmarks (�������)")]
    public GameObject redCheckmark;
    public GameObject greenCheckmark;
    public GameObject whiteCheckmark;
    public GameObject defaultCheckmark;

    private void Start()
    {
        LoadPurchasedItems(); // ��������� ���������� � ��������� �����
        LoadSelectedSprite();
        UpdateButtonsAndCheckmarks(); // ��������� ������ � ���������� ����� ����� ������
    }

    private void LoadPurchasedItems()
    {
        hasRedBall = PlayerPrefs.GetInt("HasRedBall", 0) == 1;
        hasGreenBall = PlayerPrefs.GetInt("HasGreenBall", 0) == 1;
        hasWhiteBall = PlayerPrefs.GetInt("HasWhiteBall", 0) == 1;
    }

    private void SavePurchasedItems()
    {
        PlayerPrefs.SetInt("HasRedBall", hasRedBall ? 1 : 0);
        PlayerPrefs.SetInt("HasGreenBall", hasGreenBall ? 1 : 0);
        PlayerPrefs.SetInt("HasWhiteBall", hasWhiteBall ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SetBallSprite(Sprite newSprite)
    {
        if (ballPrefab != null)
        {
            SpriteRenderer ballRenderer = ballPrefab.GetComponent<SpriteRenderer>();
            if (ballRenderer != null)
            {
                ballRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer �� ������ �� ������� ����!");
            }
        }
        else
        {
            Debug.LogError("������ ���� �� ��������!");
        }
    }

    public void BuyRedBall()
    {
        if (hasRedBall)
        {
            SelectBall(redBallSprite);
            return;
        }

        if (GameManager.Instance.score >= redBallPrice)
        {
            GameManager.Instance.score -= redBallPrice;
            PlayerPrefs.SetInt("HighScore", GameManager.Instance.score); // ���������� ��� �� ����, ��� � � GameManager
            PlayerPrefs.Save();
            hasRedBall = true;
            SavePurchasedItems();
            SelectBall(redBallSprite);
            UpdateButtonsAndCheckmarks(); // ����� ������� ��������� ��������� ����������
        }
        else
        {
            Debug.Log("������������ ����� ��� �������� ����.");
        }
    }

    public void BuyGreenBall()
    {
        if (hasGreenBall)
        {
            SelectBall(greenBallSprite);
            return;
        }

        if (GameManager.Instance.score >= greenBallPrice)
        {
            GameManager.Instance.score -= greenBallPrice;
            PlayerPrefs.SetInt("HighScore", GameManager.Instance.score); // ���������� ��� �� ����, ��� � � GameManager
            PlayerPrefs.Save();
            hasGreenBall = true;
            SavePurchasedItems();
            SelectBall(greenBallSprite);
            UpdateButtonsAndCheckmarks(); // ����� ������� ��������� ��������� ����������
        }
        else
        {
            Debug.Log("������������ ����� ��� ������� ����.");
        }
    }

    public void BuyWhiteBall()
    {
        if (hasWhiteBall)
        {
            SelectBall(whiteBallSprite);
            return;
        }

        if (GameManager.Instance.score >= whiteBallPrice)
        {
            GameManager.Instance.score -= whiteBallPrice;
            //  ��������� ����� �������� score � PlayerPrefs
            PlayerPrefs.SetInt("HighScore", GameManager.Instance.score); // ���������� ��� �� ����, ��� � � GameManager
            PlayerPrefs.Save();
            hasWhiteBall = true;
            SavePurchasedItems();
            SelectBall(whiteBallSprite);
            UpdateButtonsAndCheckmarks(); // ����� ������� ��������� ��������� ����������
        }
        else
        {
            Debug.Log("������������ ����� ��� ������ ����.");
        }
    }

    public void ResetPurchases()
    {
        hasRedBall = false;
        hasGreenBall = false;
        hasWhiteBall = false;
        SavePurchasedItems();
        UpdateButtonsAndCheckmarks();
        SelectBall(defaultBallSprite); // ������������ � ������������ ����
    }

    public void UseDefaultBall()
    {
        SelectBall(defaultBallSprite);
    }

    private void SelectBall(Sprite selectedSprite)
    {
        SaveSelectedSprite(selectedSprite);
        SetBallSprite(selectedSprite);
        UpdateButtonsAndCheckmarks(); // ����������� �������� ���������� ����� ������ ������ ����
    }

    private void SaveSelectedSprite(Sprite sprite)
    {
        string spriteName = sprite.name;
        PlayerPrefs.SetString(SelectedSpriteKey, spriteName);
        PlayerPrefs.Save();
    }

    private void LoadSelectedSprite()
    {
        if (PlayerPrefs.HasKey(SelectedSpriteKey))
        {
            string spriteName = PlayerPrefs.GetString(SelectedSpriteKey);
            Debug.Log("����������� ������: " + spriteName);

            if (spriteName == redBallSprite.name)
            {
                SetBallSprite(redBallSprite);
            }
            else if (spriteName == greenBallSprite.name)
            {
                SetBallSprite(greenBallSprite);
            }
            else if (spriteName == whiteBallSprite.name)
            {
                SetBallSprite(whiteBallSprite);
            }
            else if (spriteName == defaultBallSprite.name)
            {
                SetBallSprite(defaultBallSprite);
            }
            else
            {
                SetBallSprite(defaultBallSprite);
            }
        }
        else
        {
            SetBallSprite(defaultBallSprite);
        }
    }

    // ��������� ������ � ���������� � ����������� �� �������� ������ ������
    private void UpdateButtonsAndCheckmarks()
    {
        if (redButtons != null && greenButtons != null && whiteButtons != null &&
           redCheckmark != null && greenCheckmark != null && whiteCheckmark != null)
        {
            UpdateButtonState(redButtons, hasRedBall);
            UpdateButtonState(greenButtons, hasGreenBall);
            UpdateButtonState(whiteButtons, hasWhiteBall);

            UpdateCheckmarkState(redCheckmark, hasRedBall, GetCurrentSprite() == redBallSprite);
            UpdateCheckmarkState(greenCheckmark, hasGreenBall, GetCurrentSprite() == greenBallSprite);
            UpdateCheckmarkState(whiteCheckmark, hasWhiteBall, GetCurrentSprite() == whiteBallSprite);
            UpdateCheckmarkState(defaultCheckmark, true, GetCurrentSprite() == defaultBallSprite); // ��������� ������� ������ ��������
        }
        else
        {
            return;
        }
    }

    // ��������� ���������� ���������� �������
    private void UpdateCheckmarkState(GameObject checkmark, bool available, bool isSelected)
    {
        if (!checkmark || !available) return;

        if (isSelected)
        {
            checkmark.SetActive(true); // ������� ������������ ������ ��� ��������� ���������� ����
        }
        else
        {
            checkmark.SetActive(false); // �������������� ������� ��� ��������� �����
        }
    }

    // ��������� �������� ������������� ������� ����
    private Sprite GetCurrentSprite()
    {
        if (ballPrefab != null)
        {
            SpriteRenderer renderer = ballPrefab.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                return renderer.sprite;
            }
        }
        return null;
    }

    // ���������� ���������� �������� ������
    private void UpdateButtonState(GameObject[] buttons, bool purchased)
    {
        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.SetActive(purchased); // ������ �������� ������ ���� ��� ������
            }
        }
    }
}