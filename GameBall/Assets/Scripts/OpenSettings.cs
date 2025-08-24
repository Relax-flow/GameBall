using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public GameObject[] hideGameObject;
    public GameObject[] showGameObject;



    public void Hide()
    {
        for (int i = 0; i < hideGameObject.Length; i++)
        {
            hideGameObject[i].SetActive(false);
        }
    }

    public void Show()
    {
        for (int i = 0;i < showGameObject.Length; i++)
        {
            showGameObject[i].SetActive(true);
        }
    }
}
