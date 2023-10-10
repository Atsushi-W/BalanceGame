using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>
/// テキストをアニメーションをさせるコンポーネント
/// </summary>

public class TextAnimation : MonoBehaviour
{
    private TextMeshProUGUI _text = default;
    private DOTweenTMPAnimator _tmpAnimator;
    [SerializeField]
    private float _textDuration;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _tmpAnimator = new DOTweenTMPAnimator(_text);
        Play(_textDuration);
    }

    private void Play(float duration)
    {
        for (var i = 0; i < _tmpAnimator.textInfo.characterCount; i++)
        {
            // エラーになるため空文字(空白)はスルー
            if (!_tmpAnimator.textInfo.characterInfo[i].isVisible)
            {
                continue;
            }

            // 偶数番目と奇数番目で方向を変える
            var sign = Mathf.Sign(i % 2 - 0.5f);
            DOTween.Sequence()
                .Append(_tmpAnimator.DOOffsetChar(i, Vector3.up * 10 * sign, duration / 2).SetEase(Ease.OutFlash, 2))
                .Append(_tmpAnimator.DOOffsetChar(i, Vector3.down * 10 * sign, duration / 2).SetEase(Ease.OutFlash, 2))
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
