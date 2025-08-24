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
        //Убедимся, что GameData инициализирован при загрузке сцены с мячом.
        if (string.IsNullOrEmpty(GameData.SelectedSprite))
        {
            LoadSavedSprite();
        }
    }

    void Start()
    {

        // Получаем SpriteRenderer из GameObject ball
        skinchange = ball.GetComponent<SpriteRenderer>();

        // Проверка, что SpriteRenderer найден
        if (skinchange == null)
        {
            Debug.LogError("SpriteRenderer не найден на объекте ball!");
            return;
        }

        //LoadSavedSprite();  // Загружаем спрайт в Start()
        SetCurrentSprite(); //Устанавливаем спрайт, основываясь на GameData.SelectedSpriteName

    }

    private void LoadSavedSprite()
    {
        // Загружаем имя выбранного спрайта из PlayerPrefs при первом запуске или если GameData еще не инициализирован
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
            PlayerPrefs.SetInt("Score", GameData.Score); // Сохраняем обновленные очки
            PlayerPrefs.Save();

            // Помечаем спрайт как купленный
            PlayerPrefs.SetInt(hasSpriteKey, 1); // 1 означает true
            PlayerPrefs.Save();

            SetSprite(sprite);
            GameData.SelectedSprite = spriteName; // Исправлено
            PlayerPrefs.SetString(SelectedSpriteKey, spriteName);
            PlayerPrefs.Save(); // Важно сохранить изменения!

            Debug.Log("Куплен спрайт: " + spriteName);
        }
        else
        {
            Debug.Log("Недостаточно очков для покупки спрайта: " + spriteName);
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

        //Проверяем, был ли спрайт куплен
        if (!string.IsNullOrEmpty(hasSpriteKey) && PlayerPrefs.GetInt(hasSpriteKey, 0) == 1) //Проверяем значение PlayerPrefs, чтобы узнать, был ли он куплен
        {
            // Спрайт был куплен, устанавливаем его
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
            GameData.SelectedSprite = spriteName; // Исправлено
            PlayerPrefs.SetString(SelectedSpriteKey, spriteName);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Этот спрайт не был куплен: " + spriteName);
            //Если спрайт не был куплен, вы можете установить дефолтный спрайт или что-то еще.
            SetSprite(defaultSprite);
            GameData.SelectedSprite = "Default"; // Исправлено
            PlayerPrefs.SetString(SelectedSpriteKey, "Default");
            PlayerPrefs.Save();
        }
    }

    //Устанавливает спрайт, основываясь на GameData.SelectedSpriteName
    private void SetCurrentSprite()
    {
        switch (GameData.SelectedSprite) // Исправлено
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
    public static int Score { get; set; } = PlayerPrefs.GetInt("Score", 0); //Загружаем очки из PlayerPrefs при запуске
    public static string SelectedSprite { get; set; } // Исправлено
}
