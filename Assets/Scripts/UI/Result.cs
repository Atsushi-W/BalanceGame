using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// ���U���g�̃X�R�A��\������R���|�[�l���g
/// </summary>

public class Result : MonoBehaviour
{
    private TextMeshProUGUI _resultText;
    [SerializeField]
    private float _scoreTimeDuration;
    [SerializeField]
    private GameObject _retryButton;
    [SerializeField]
    private GameObject _highScoreObject;

    private void Awake()
    {
        _resultText = GetComponent<TextMeshProUGUI>();
        _retryButton.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.OnResultScore += ResultScore;
    }

    private void ResultScore(int score)
    {
        if (score > 0)
        {
            StartCoroutine(ScoreAnimation(0, score,_scoreTimeDuration));
        }
        else
        {
            SetRestultScore(score);
        }
    }

    private IEnumerator ScoreAnimation(int startscore, int endscore, float duration)
    {
        // �J�n����
        float startTime = Time.time;
        //�I������
        float endTime = startTime + duration;
        
        while(Time.time < endTime)
        {
            // ���݂̎��Ԃ̊���
            float timeRate = (Time.time - startTime) / duration;

            // ���l���X�V
            float updateValue = ((endscore - startscore) * timeRate + startscore);
            
            // �e�L�X�g�̍X�V
            _resultText.text = "��" + updateValue.ToString("N0");

            // 1�t���[���҂�
            yield return null;
        }

        SetRestultScore(endscore);
    }

    private void SetRestultScore(int score)
    {
        // �ŏI�X�R�A�̕\��
        _resultText.text = "��" + score.ToString("N0");

        AudioManager.Instance.PlaySE(AudioManager.SEName.Result);
        _retryButton.SetActive(true);

        // �n�C�X�R�A���m�F
        int highscore = PlayerPrefs.GetInt("moneytsumscore", 0);
        if (highscore < score)
        {
            _highScoreObject.SetActive(true);
            // �n�C�X�R�A��ۑ�
            PlayerPrefs.SetInt("moneytsumscore", score);
            PlayerPrefs.Save();
        }
    }

    private void ResetScore()
    {
        _resultText.text = "��0";
    }
}
