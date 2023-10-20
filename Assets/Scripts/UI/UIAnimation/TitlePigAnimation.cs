using UnityEngine;
using DG.Tweening;

/// <summary>
/// �^�C�g����ʂ̓؂��A�j���[�V����������R���|�[�l���g
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
    /// �؂��N���b�N���ꂽ���A�X�P�[���l��߂�
    /// </summary>
    public void ClickPig()
    {
        // �e�L�X�g���\��
        _textObject.SetActive(false);

        // �X�P�[���l��߂�
        transform.DOKill();
        transform.DOScale(1f, 1f)
            .SetRecyclable(true)
            .SetEase(Ease.OutQuart);

        // �^�C�g��BGM���Đ�
        AudioManager.Instance.PlayBGM(AudioManager.BGMName.Title);
    }

    /// <summary>
    /// �^�C�g�����̃A�j���[�V����
    /// </summary>
    private void StartAnimation()
    {
        transform.DOScale(0.9f, 1f)
            .SetRecyclable(true)
            .SetEase(Ease.OutQuart)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
