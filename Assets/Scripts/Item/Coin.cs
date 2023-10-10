using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使用する硬貨のベースクラス
/// </summary>
public class Coin : MonoBehaviour
{
    // 硬貨一覧
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
    /// コインの選択状態を設定
    /// </summary>
    /// <param name="isSelect">コインの選択状態</param>
    public void SetIsSelect(bool isSelect)
    {
        IsSelect = isSelect;
        SelectSprite.SetActive(isSelect);
    }

    /// <summary>
    /// コインのIDとスプライトを設定
    /// </summary>
    /// <param name="name">硬貨名</param>
    public void SetCoin(Coin.CoinName name)
    {
        ID = (int)name;
        _spriteRenderer.sprite = _coinTexture[ID];
    }

    /// <summary>
    /// コインのIDとスプライトを設定
    /// </summary>
    /// <param name="id">ID</param>
    public void SetCoin(int id)
    {
        ID = id;
        _spriteRenderer.sprite = _coinTexture[ID];
    }

    /// <summary>
    /// マウスが押された時に実行
    /// </summary>
    private void OnMouseDown()
    {
        GameManager.Instance.CoinDown(this);
    }

    /// <summary>
    /// マウスを押した状態で重なった時に実行
    /// </summary>
    private void OnMouseEnter()
    {
        GameManager.Instance.CoinEnter(this);
    }

    /// <summary>
    /// マウスを離した時に実行
    /// </summary>
    private void OnMouseUp()
    {
        GameManager.Instance.CoinUp();
    }
}
