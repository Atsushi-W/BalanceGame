using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// �R�C���������ɕ\������X�R�A
/// </summary>
public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// �X�R�A�\��
    /// </summary>
    /// <param name="score">�\������X�R�A</param>
    public void SetScoreDesplay(int score)
    {
        _scoreText.text = "��" + score.ToString("N0");
        StartCoroutine("DisplayTime");
    }

    /// <summary>
    /// �X�R�A�\�����ԂƃA�j���[�V�����ݒ�
    /// </summary>
    /// <returns></returns>
    IEnumerator DisplayTime()
    {
        // �J���[�̏�����
        _scoreText.color = Color.white;
        // �����(Y��)�ɏ㏸���A���X�ɃA���t�@�l��0�ɂ���
        transform.DOKill();
        DOTween.Sequence()
            .Append(transform.DOMoveY(transform.position.y + 0.5f, 0.5f))
            .Append(_scoreText.DOFade(0f,0.5f));
        
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
