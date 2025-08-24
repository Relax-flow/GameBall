using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForCollider : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private MyEnum _triggerType; // ��������� ���� ��� ������ ���� �������� � ����������
    private enum MyEnum
    {
        Top,
        Bottom
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_triggerType == MyEnum.Top)
            _target.SetActive(false);
        if (_triggerType == MyEnum.Bottom)
            _target.SetActive(true);
    }
    
}
