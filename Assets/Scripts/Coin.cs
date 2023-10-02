using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
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
    private Sprite[] coinTexture;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        GameManager.Instance.CoinDown(this);
    }

    private void OnMouseEnter()
    {
        GameManager.Instance.CoinEnter(this);
    }

    private void OnMouseUp()
    {
        GameManager.Instance.CoinUp();
    }
    public void SetIsSelect(bool isSelect)
    {
        IsSelect = isSelect;
        SelectSprite.SetActive(isSelect);
    }

    public void SetCoin(Coin.CoinName name)
    {
        ID = (int)name;
        spriteRenderer.sprite = coinTexture[ID];
    }

    public void SetCoin(int id)
    {
        ID = id;
        spriteRenderer.sprite = coinTexture[ID];
    }
}
