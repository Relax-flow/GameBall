using UnityEngine;
using System.Collections;
using System;

public class RotateAndReset : MonoBehaviour
{
    public float rotationAngle = -90f; // ���� �������� �� ��� Z
    public float rotationDuration = 0.5f; // ����� ��������
    public float coolDownTime = 1.0f; // ����� ���������� ����� ������� �����

    private Quaternion _initialRotation; // �������� ������� �������
    private bool _isRotating = false; // ����, �����������, ����������� �� ��������

    public float pushForce = 10f;
    public AudioSource audioClip;

    private void Start()
    {
        // ��������� �������� ������� �������
        _initialRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        // ��������� ������� �� ����� (��������� ����������) ��� ������ (��������)
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            // ���� �������� ��� �� �����������, �������� ��������
            if (!_isRotating)
            {
                StartCoroutine(RotateAndResetCoroutine());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (!Input.GetKeyDown(KeyCode.Space) && Input.touchCount <= 0) return;
          
            Vector2 direction = collision.transform.position - transform.position;
            direction = direction.normalized;
            ballRb.AddForce(direction * pushForce, ForceMode2D.Impulse);
                
            
        }
    }

    // �������� ��� ���������� �������� � �������� � �������� ���������
    private IEnumerator RotateAndResetCoroutine()
    {
        _isRotating = true;

        // ������� ������
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < rotationDuration)
        {
            audioClip.Play();
            transform.rotation = Quaternion.Slerp(startRotation, startRotation * targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����������� ������ �������� ��������
        transform.rotation = startRotation * targetRotation;

        // ���������� ������ � �������� ���������
        elapsedTime = 0f;
        startRotation = transform.rotation;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, _initialRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����������� ������ �������� ��������
        transform.rotation = _initialRotation;

        // ���� �������� �����
        yield return new WaitForSeconds(coolDownTime);
        _isRotating = false;
    }
}
