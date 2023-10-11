using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// リザルトのスコアを表示するコンポーネント
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
        // 開始時間
        float startTime = Time.time;
        //終了時間
        float endTime = startTime + duration;
        
        while(Time.time < endTime)
        {
            // 現在の時間の割合
            float timeRate = (Time.time - startTime) / duration;

            // 数値を更新
            float updateValue = ((endscore - startscore) * timeRate + startscore);
            
            // テキストの更新
            _resultText.text = "￥" + updateValue.ToString("N0");

            // 1フレーム待つ
            yield return null;
        }

        SetRestultScore(endscore);
    }

    private void SetRestultScore(int score)
    {
        // 最終スコアの表示
        _resultText.text = "￥" + score.ToString("N0");

        AudioManager.Instance.PlaySE(AudioManager.SEName.Result);
        _retryButton.SetActive(true);

        // ハイスコアを確認
        int highscore = PlayerPrefs.GetInt("moneytsumscore", 0);
        if (highscore < score)
        {
            _highScoreObject.SetActive(true);
            // ハイスコアを保存
            PlayerPrefs.SetInt("moneytsumscore", score);
            PlayerPrefs.Save();
        }
    }

    private void ResetScore()
    {
        _resultText.text = "￥0";
    }
}
