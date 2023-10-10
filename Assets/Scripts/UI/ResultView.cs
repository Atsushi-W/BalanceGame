using UnityEngine;
using DG.Tweening;

/// <summary>
/// ���U���g�̃X�R�A�\��������E�B���h�E���A�j���[�V����������R���|�[�l���g
/// </summary>
public class ResultView : MonoBehaviour
{
    private RectTransform _rectTransform;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        GameManager.Instance.OnResultScore += ViewResultWindow;
    }

    private void ViewResultWindow(int score)
    {
        _rectTransform.anchoredPosition = new Vector3(0, 700, 0);
        _rectTransform.DOKill();
        _rectTransform.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBounce);
    }

}
