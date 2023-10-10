using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �g�p����d�݂̃x�[�X�N���X
/// </summary>
public class Coin : MonoBehaviour
{
    // �d�݈ꗗ
    public enum CoinName
    {
        One,
        Five,
        Ten,
        Fifty,
        Hundred,
        FiveHundreds,
    }

    public int ID;
    public GameObject SelectSprite;

    public bool IsSelect { get; private set; }

    [SerializeField]
    private Sprite[] _coinTexture;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �R�C���̑I����Ԃ�ݒ�
    /// </summary>
    /// <param name="isSelect">�R�C���̑I�����</param>
    public void SetIsSelect(bool isSelect)
    {
        IsSelect = isSelect;
        SelectSprite.SetActive(isSelect);
    }

    /// <summary>
    /// �R�C����ID�ƃX�v���C�g��ݒ�
    /// </summary>
    /// <param name="name">�d�ݖ�</param>
    public void SetCoin(Coin.CoinName name)
    {
        ID = (int)name;
        _spriteRenderer.sprite = _coinTexture[ID];
    }

    /// <summary>
    /// �R�C����ID�ƃX�v���C�g��ݒ�
    /// </summary>
    /// <param name="id">ID</param>
    public void SetCoin(int id)
    {
        ID = id;
        _spriteRenderer.sprite = _coinTexture[ID];
    }

    /// <summary>
    /// �}�E�X�������ꂽ���Ɏ��s
    /// </summary>
    private void OnMouseDown()
    {
        GameManager.Instance.CoinDown(this);
    }

    /// <summary>
    /// �}�E�X����������Ԃŏd�Ȃ������Ɏ��s
    /// </summary>
    private void OnMouseEnter()
    {
        GameManager.Instance.CoinEnter(this);
    }

    /// <summary>
    /// �}�E�X�𗣂������Ɏ��s
    /// </summary>
    private void OnMouseUp()
    {
        GameManager.Instance.CoinUp();
    }
}
