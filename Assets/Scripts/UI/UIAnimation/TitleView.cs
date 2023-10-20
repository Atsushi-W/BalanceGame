using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleView : MonoBehaviour
{
    // 動作対象一覧
    private enum AnimationObject
    {
        TitleView,
        Button
    }

    [SerializeField]
    private AnimationObject _animationObject;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        switch(_animationObject)
        {
            case AnimationObject.TitleView:
                TitleAnimation();
                break;
            case AnimationObject.Button:
                ButtonAnimation();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// タイトルアニメーションの再生
    /// </summary>
    private void TitleAnimation()
    {
        _rectTransform.anchoredPosition = new Vector3(0, 700, 0);
        _rectTransform.DOKill();
        _rectTransform.DOLocalMoveY(180f, 0.5f).SetEase(Ease.OutBounce);
    }

    /// <summary>
    /// ボタンアニメーションの再生
    /// </summary>
    private void ButtonAnimation()
    {
        _rectTransform.anchoredPosition = new Vector3(0, -700, 0);
        _rectTransform.DOKill();
        _rectTransform.DOLocalMoveY(-200f, 0.5f).SetEase(Ease.OutBounce);

        _rectTransform.DOScale(0.1f, 1f)
            .SetRelative(true)
            .SetEase(Ease.OutQuart)
            .SetLoops(-1, LoopType.Restart);
    }
}
