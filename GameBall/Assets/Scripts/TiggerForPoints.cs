using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class TriggerForPoints : MonoBehaviour
{
    private const string MoneyKey = "Money";
    private const string LevelCompletedKeyPrefix = "LevelCompleted_";

    private Collider2D _collider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject[] pointScoreObjects;
    public static int money { get; private set; } = 0;
    private bool isLevelCompleted = false;
    public static bool checkwin = false;

    void Awake()
    {
        // Загружаем предыдущее состояние уровня
        isLevelCompleted = PlayerPrefs.GetInt(LevelCompletedKeyPrefix + GetLevelName(), 0) == 1;

        // Переинициализируем объекты при повторном заходе на уровень
        ResetCurrentState();

        _collider = GetComponent<Collider2D>();

        if (_collider == null)
        {
            Debug.LogError("Collider2D не найден на этом объекте!");
        }

        if (pointScoreObjects == null || pointScoreObjects.Length == 0)
        {
            Debug.LogError("Массив pointScoreObjects не инициализирован или пуст!");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isLevelCompleted)
        {
            _collider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        if (!isLevelCompleted)
        {
            StartCoroutine(TimeForEnabled());
        }
    }

    private void IncreaseMoney()
    {
        if (!isLevelCompleted && money < pointScoreObjects.Length)
        {
            money++;
            UpdatePointScoreObjects();

            if (money >= 4)
            {
                MarkLevelCompleted();
            }
        }
        else
        {
            Debug.Log("Достигнут максимум денег или уровень уже завершён.");
        }
    }

    IEnumerator TimeForEnabled()
    {
        IncreaseMoney();
        _collider.enabled = false;
        yield return new WaitForSeconds(1.4f);
        _collider.enabled = true;
    }

    private void UpdatePointScoreObjects()
    {
        if (isLevelCompleted)
        {
            foreach (var obj in pointScoreObjects)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            return;
        }

        for (int i = 0; i < pointScoreObjects.Length; i++)
        {
            if (pointScoreObjects[i] != null)
            {
                pointScoreObjects[i].SetActive(i >= money); // Показывать доступные объекты
            }
        }
    }

    private void MarkLevelCompleted()
    {
        if (!isLevelCompleted)
        {
            isLevelCompleted = true;
            PlayerPrefs.SetInt(LevelCompletedKeyPrefix + GetLevelName(), 1);
            PlayerPrefs.Save();
        }
        checkwin = true;
        UpdatePointScoreObjects();
    }

    private void ResetCurrentState()
    {
        // Сброс общего числа очков и активности объектов
        money = 0;
        isLevelCompleted = false;
        checkwin = false;
        UpdatePointScoreObjects(); // Возвращаем все объекты обратно
    }

    private string GetLevelName()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public static void ResetMoney()
    {
        money = 0;
        PlayerPrefs.DeleteKey(MoneyKey);
        PlayerPrefs.Save();
    }

    public static void ResetAllLevelProgress()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            string levelCompletedKey = "LevelCompleted_" + sceneName; // Correct key

            PlayerPrefs.DeleteKey("FirstCompletion_" + sceneName); //delete first completeion key
            PlayerPrefs.DeleteKey(LevelCompletedKeyPrefix + sceneName);
        }

        PlayerPrefs.Save();
        Debug.Log("Прогресс всех уровней сброшен!");
    }

    private void OnApplicationQuit()
    {
        SaveMoney();
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyKey, money);
        PlayerPrefs.Save();
    }

    private void LoadMoney()
    {
        if (PlayerPrefs.HasKey(MoneyKey))
        {
            money = PlayerPrefs.GetInt(MoneyKey);
        }
        else
        {
            money = 0;
        }
    }
}
