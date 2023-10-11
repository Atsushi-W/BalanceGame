using UnityEngine;
using TMPro;

/// <summary>
/// �n�C�X�R�A�\���̃R���|�[�l���g
/// </summary>
public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnHighScore += UpdateHighScore;
        int highscore = PlayerPrefs.GetInt("moneytsumscore", 0);
        UpdateHighScore(highscore);
    }

    private void UpdateHighScore(int score)
    {
        _scoreText.text = "��" + score.ToString("N0");
    }

}
