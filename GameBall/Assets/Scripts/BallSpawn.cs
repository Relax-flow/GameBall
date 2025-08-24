using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject ballPrefab;  // ������ ���� ��� ��������
    public Transform spawnPoint; // �����, ��� ����� ������ ���
    public ShopManager shopManager; // ������ �� ShopManager (���������� � ����������)

    private GameObject currentBall; // ������ �� ������� ��� �� �����.

    // Start is called before the first frame update
    void Start()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("������ ���� �� �������� � BallSpawner!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("����� ������ �� ��������� � BallSpawner!");
            return;
        }

        if (shopManager == null)
        {
            Debug.LogError("ShopManager �� �������� � BallSpawner!");
            return;
        }

        SpawnBall(); // ������� ��� ��� ������ �����
    }

    // ������� ��� �������� ����
    public void SpawnBall()
    {
        // ���������� ���������� ���, ���� �� ����
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        // ������� ����� ��� �� �������
        currentBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);

        // �������� ��������� SpriteRenderer � ������ ����
        SpriteRenderer ballSpriteRenderer = currentBall.GetComponent<SpriteRenderer>();

        // ���������, ��� ��������� SpriteRenderer ����������
        if (ballSpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer �� ������ �� ������� ����!");
            return;
        }

        // ��������� ��� ���������� ������� �� PlayerPrefs
        string selectedSpriteName = PlayerPrefs.GetString("SelectedBallSprite", "");

        // ������������� ������ ���� � ������������ � ��������� ��������
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
                // ���� ��� ������� �� �������, ������������� ��������� ������
                ballSpriteRenderer.sprite = shopManager.defaultBallSprite;
            }
        }
        else
        {
            // ���� � PlayerPrefs ������ ���, ������������� ��������� ������
            ballSpriteRenderer.sprite = shopManager.defaultBallSprite;
        }
    }
}
