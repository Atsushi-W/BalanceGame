using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TextAnimationControler : MonoBehaviour
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

    public void Play(float duration)
    {
        for (var i = 0; i < _tmpAnimator.textInfo.characterCount; i++)
        {
            // ‹ó•¶Žš(‹ó”’)‚ÍƒXƒ‹[
            if (!_tmpAnimator.textInfo.characterInfo[i].isVisible)
            {
                continue;
            }

            // ‹ô””Ô–Ú‚ÆŠï””Ô–Ú‚Å•ûŒü‚ð•Ï‚¦‚é
            var sign = Mathf.Sign(i % 2 - 0.5f);
            DOTween.Sequence()
                .Append(_tmpAnimator.DOOffsetChar(i, Vector3.up * 10 * sign, duration / 2).SetEase(Ease.OutFlash, 2))
                .Append(_tmpAnimator.DOOffsetChar(i, Vector3.down * 10 * sign, duration / 2).SetEase(Ease.OutFlash, 2))
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
