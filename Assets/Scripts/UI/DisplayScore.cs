using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// コイン消去時に表示するスコア
/// </summary>
public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// スコア表示
    /// </summary>
    /// <param name="score">表示するスコア</param>
    public void SetScoreDesplay(int score)
    {
        _scoreText.text = "￥" + score.ToString("N0");
        StartCoroutine("DisplayTime");
    }

    /// <summary>
    /// スコア表示時間とアニメーション設定
    /// </summary>
    /// <returns></returns>
    IEnumerator DisplayTime()
    {
        // カラーの初期化
        _scoreText.color = Color.white;
        // 上方向(Y軸)に上昇しつつ、徐々にアルファ値を0にする
        transform.DOKill();
        DOTween.Sequence()
            .Append(transform.DOMoveY(transform.position.y + 0.5f, 0.5f))
            .Append(_scoreText.DOFade(0f,0.5f));
        
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
