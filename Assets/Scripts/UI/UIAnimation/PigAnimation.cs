using UnityEngine;
using DG.Tweening;

/// <summary>
/// �X�R�A�l�����ɓ؂��A�j���[�V����������R���|�[�l���g
/// </summary>

public class PigAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        GameManager.Instance.OnScoreUpdate += PlayPigAnimation;
    }

    private void PlayPigAnimation(int score)
    {
        if (score != 0)
        {
            _rectTransform.DOKill();
            _rectTransform.DOPunchScale(
                Vector2.one * 0.2f,
                1.0f
            );
        }
    }
}
