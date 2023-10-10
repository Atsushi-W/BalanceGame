using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// �{�^������������A�j���[�V������������R���|�[�l���g
/// </summary>
public class ButtonAnimation : MonoBehaviour
{
    void Awake()
    {
        StartStrongButtonAnim();
    }

    void StartStrongButtonAnim()
    {
        transform.DOScale(0.1f, 1f)
        .SetRelative(true)
        .SetEase(Ease.OutQuart)
        .SetLoops(-1, LoopType.Restart);
    }
}
