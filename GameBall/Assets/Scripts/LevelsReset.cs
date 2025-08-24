using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsReset : MonoBehaviour
{
    public void ResetLevel()
    {
        TriggerForPoints.ResetAllLevelProgress();

        // �������� ������ � GameManager � ���������� ����
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            //gameManager.ResetScore();
            Debug.Log("���� ��������.");
        }
        else
        {
            Debug.LogWarning("GameManager �� ������ �� �����!");
        }

        // �������� ������ � ShopManager � ���������� �������
        ShopManager shopManager = FindObjectOfType<ShopManager>();
        if (shopManager != null)
        {
            //shopManager.ResetPurchases();
            Debug.Log("������� ��������.");
        }
        else
        {
            Debug.LogWarning("ShopManager �� ������ �� �����!");
        }
    }
}
