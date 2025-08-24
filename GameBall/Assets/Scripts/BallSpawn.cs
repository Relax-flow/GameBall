using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject ballPrefab;  // Префаб мяча для создания
    public Transform spawnPoint; // Точка, где будет создан мяч
    public ShopManager shopManager; // Ссылка на ShopManager (перетащите в инспекторе)

    private GameObject currentBall; // Ссылка на текущий мяч на сцене.

    // Start is called before the first frame update
    void Start()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Префаб мяча не назначен в BallSpawner!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Точка спавна не назначена в BallSpawner!");
            return;
        }

        if (shopManager == null)
        {
            Debug.LogError("ShopManager не назначен в BallSpawner!");
            return;
        }

        SpawnBall(); // Создаем мяч при старте сцены
    }

    // Функция для создания мяча
    public void SpawnBall()
    {
        // Уничтожаем предыдущий мяч, если он есть
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        // Создаем новый мяч из префаба
        currentBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        // Получаем компонент SpriteRenderer у нового мяча
        SpriteRenderer ballSpriteRenderer = currentBall.GetComponent<SpriteRenderer>();

        // Проверяем, что компонент SpriteRenderer существует
        if (ballSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer не найден на префабе мяча!");
            return;
        }

        // Загружаем имя выбранного спрайта из PlayerPrefs
        string selectedSpriteName = PlayerPrefs.GetString("SelectedBallSprite", "");

        // Устанавливаем спрайт мяча в соответствии с выбранным спрайтом
        if (!string.IsNullOrEmpty(selectedSpriteName))
        {
            if (selectedSpriteName == shopManager.redBallSprite.name)
            {
                ballSpriteRenderer.sprite = shopManager.redBallSprite;
            }
            else if (selectedSpriteName == shopManager.greenBallSprite.name)
            {
                ballSpriteRenderer.sprite = shopManager.greenBallSprite;
            }
            else if (selectedSpriteName == shopManager.whiteBallSprite.name)
            {
                ballSpriteRenderer.sprite = shopManager.whiteBallSprite;
            }
            else if (selectedSpriteName == shopManager.defaultBallSprite.name)
            {
                ballSpriteRenderer.sprite = shopManager.defaultBallSprite;
            }
            else
            {
                // Если имя спрайта не найдено, устанавливаем дефолтный спрайт
                ballSpriteRenderer.sprite = shopManager.defaultBallSprite;
            }
        }
        else
        {
            // Если в PlayerPrefs ничего нет, устанавливаем дефолтный спрайт
            ballSpriteRenderer.sprite = shopManager.defaultBallSprite;
        }
    }
}
