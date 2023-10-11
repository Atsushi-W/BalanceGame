using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>
/// テキストをアニメーションをさせるコンポーネント
/// </summary>
public class NewRecordTextAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Play();
    }

    private void Play()
    {
        _rectTransform.DOLocalMoveY(10f, 0.4f)
            .SetRelative(true)
            .SetEase(Ease.OutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
