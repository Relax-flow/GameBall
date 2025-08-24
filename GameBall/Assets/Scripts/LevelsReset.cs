using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsReset : MonoBehaviour
{
    public void ResetLevel()
    {
        TriggerForPoints.ResetAllLevelProgress();

        // Получаем доступ к GameManager и сбрасываем очки
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            //gameManager.ResetScore();
            Debug.Log("Очки сброшены.");
        }
        else
        {
            Debug.LogWarning("GameManager не найден на сцене!");
        }

        // Получаем доступ к ShopManager и сбрасываем покупки
        ShopManager shopManager = FindObjectOfType<ShopManager>();
        if (shopManager != null)
        {
            //shopManager.ResetPurchases();
            Debug.Log("Покупки сброшены.");
        }
        else
        {
            Debug.LogWarning("ShopManager не найден на сцене!");
        }
    }
}
