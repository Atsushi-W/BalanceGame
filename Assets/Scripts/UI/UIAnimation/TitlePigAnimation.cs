using UnityEngine;
using DG.Tweening;

/// <summary>
/// タイトル画面の豚をアニメーションさせるコンポーネント
/// </summary>
public class TitlePigAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject _textObject;

    private void Awake()
    {
        StartAnimation();
    }

    /// <summary>
    /// 豚がクリックされた時、スケール値を戻す
    /// </summary>
    public void ClickPig()
    {
        // テキストを非表示
        _textObject.SetActive(false);

        // スケール値を戻す
        transform.DOKill();
        transform.DOScale(1f, 1f)
            .SetRecyclable(true)
            .SetEase(Ease.OutQuart);

        // タイトルBGMを再生
        AudioManager.Instance.PlayBGM(AudioManager.BGMName.Title);
    }

    /// <summary>
    /// タイトル時のアニメーション
    /// </summary>
    private void StartAnimation()
    {
        transform.DOScale(0.9f, 1f)
            .SetRecyclable(true)
            .SetEase(Ease.OutQuart)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
