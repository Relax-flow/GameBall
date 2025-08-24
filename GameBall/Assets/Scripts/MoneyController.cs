using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public Text moneyText;

    private void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = GameManager.Instance.score.ToString();
        }

        if (moneyText != null)
        {
            // �������� ��� ������� ����� ��� ������������ ����������� �����
            string currentLevelName = SceneManager.GetActiveScene().name;
            string firstCompletionKey = "FirstCompletion_" + currentLevelName;

            // ���������, ��� �� ������� ��� �������
            if (!PlayerPrefs.HasKey(firstCompletionKey))
            {
                moneyText.text = "+4";
            }
            else
            {
                moneyText.text = "+4";
            }
        }
    }
}
