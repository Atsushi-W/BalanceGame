
using UnityEngine;
using TMPro;
using System.Collections;

public class Result : MonoBehaviour
{
    private TextMeshProUGUI _resultText;
    [SerializeField]
    private float _scoreTimeDuration;
    [SerializeField]
    private GameObject _retryButton;

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

        //_resultText.text = "��" + score.ToString("N0");
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
    }

    private void ResetScore()
    {
        _resultText.text = "��0";
    }
}
