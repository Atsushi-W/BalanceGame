using UnityEngine;
using TMPro;

/// <summary>
/// �X�R�A�\���̃R���|�[�l���g
/// </summary>
public class Score : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnScoreUpdate += UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = "��" + score.ToString("N0");
    }

    private void ResetScore()
    {
        _scoreText.text = "��0";
    }
}
