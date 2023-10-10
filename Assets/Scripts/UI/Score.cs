using UnityEngine;
using TMPro;

/// <summary>
/// スコア表示のコンポーネント
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
        _scoreText.text = "￥" + score.ToString("N0");
    }

    private void ResetScore()
    {
        _scoreText.text = "￥0";
    }
}
