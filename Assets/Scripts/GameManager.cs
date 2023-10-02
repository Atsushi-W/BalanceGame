using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject CoinPrefab;

    public LineRenderer lineRenderer;

    public int CoinDestroyCount = 3;
    public float CoinConnectRange = 1.5f;

    private List<Coin> _selectCoin = new List<Coin>();
    private int _selectID = -1;
    private Coin.CoinName _selectCoinName;
    private int _spawnCoinCount = 0;

    void Start()
    {
        Instance = this;
        CoinSpawn(40);
    }

    void Update()
    {
        LineRendererUpdate();
    }

    private void LineRendererUpdate()
    {
        if (_selectCoin.Count >= 2)
        {
            lineRenderer.positionCount = _selectCoin.Count;
            lineRenderer.SetPositions(_selectCoin.Select(coin => coin.transform.position).ToArray());
            lineRenderer.gameObject.SetActive(true);
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }

    private void CoinSpawn(int count)
    {
        int startX = -2;
        int startY = 5;
        int maxX = 5;
        int x = 0, y = 0;

        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(startX + x, startY + y);
            GameObject coin = Instantiate(CoinPrefab, pos, Quaternion.identity);
            coin.GetComponent<Coin>().SetCoin((Coin.CoinName)Random.Range(0, 4));
            x++;
            if(x == maxX)
            {
                x = 0;
                y++;
            }
        }
    }

    private void NextCoinSpawn()
    {

        if ((int)Coin.CoinName.FiveHundreds <= _selectID) return;

        switch (_selectCoinName)
        {
            case Coin.CoinName.One:
                if (_selectCoin.Count >= 5)
                {
                    for (int i = 5; i <= _selectCoin.Count; i+=5)
                    {
                        GameObject coin = Instantiate(CoinPrefab, _selectCoin[i - 1].transform.position, Quaternion.identity);
                        coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                        _spawnCoinCount++;
                    }
                }
                break;
            case Coin.CoinName.Five:
                if (_selectCoin.Count >= 2)
                {
                    for (int i = 2; i <= _selectCoin.Count; i += 2)
                    {
                        GameObject coin = Instantiate(CoinPrefab, _selectCoin[i - 1].transform.position, Quaternion.identity);
                        coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                        _spawnCoinCount++;
                    }
                }
                break;
            case Coin.CoinName.Ten:
                if (_selectCoin.Count >= 5)
                {
                    for (int i = 5; i <= _selectCoin.Count; i += 5)
                    {
                        GameObject coin = Instantiate(CoinPrefab, _selectCoin[i - 1].transform.position, Quaternion.identity);
                        coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                        _spawnCoinCount++;
                    }
                }
                break;
            case Coin.CoinName.Fifty:
                if (_selectCoin.Count >= 2)
                {
                    for (int i = 2; i <= _selectCoin.Count; i += 2)
                    {
                        GameObject coin = Instantiate(CoinPrefab, _selectCoin[i - 1].transform.position, Quaternion.identity);
                        coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                        _spawnCoinCount++;
                    }
                }
                break;
            case Coin.CoinName.Hundred:
                if (_selectCoin.Count >= 5)
                {
                    for (int i = 5; i <= _selectCoin.Count; i += 5)
                    {
                        GameObject coin = Instantiate(CoinPrefab, _selectCoin[i - 1].transform.position, Quaternion.identity);
                        coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                        _spawnCoinCount++;
                    }
                }
                break;
            case Coin.CoinName.FiveHundreds:
            default:
                break;
        }
    }

    public void CoinDown(Coin coin)
    {
        _selectCoin.Add(coin);
        coin.SetIsSelect(true);

        _selectID = coin.ID;
        _selectCoinName = (Coin.CoinName)coin.ID;
    }

    public void CoinEnter(Coin coin)
    {
        if (_selectID != coin.ID) return;

        if (coin.IsSelect)
        {
            if (_selectCoin.Count >= 2 && _selectCoin[_selectCoin.Count - 2] == coin)
            {
                Coin removeCoin = _selectCoin[_selectCoin.Count - 1];
                removeCoin.SetIsSelect(false);
                _selectCoin.Remove(removeCoin);
            }
        }
        else
        {
            float length = (_selectCoin[_selectCoin.Count - 1].transform.position - coin.transform.position).magnitude;
            if (length < CoinConnectRange)
            {
                _selectCoin.Add(coin);
                coin.SetIsSelect(true);
            }
        }
    }

    public void CoinUp()
    {
        if (_selectCoin.Count >= CoinDestroyCount)
        {
            NextCoinSpawn();
            DestroyCoin(_selectCoin);
        }
        else
        {
            // “ÁŽê‚ÈÁ‚µ•û
            // 5‰~‹Ê‚Í2–‡A50‰~‹Ê‚Í2–‡‚Å‚àÁ‚¦‚é(—¼‘Ö)
            if ( (_selectID == (int)Coin.CoinName.Five || _selectID == (int)Coin.CoinName.Fifty || _selectID == (int)Coin.CoinName.FiveHundreds)
                 && _selectCoin.Count >= 2)
            {
                NextCoinSpawn();
                DestroyCoin(_selectCoin);
            }
            else
            {
                foreach (var coinItem in _selectCoin)
                {
                    coinItem.SetIsSelect(false);
                }
            }
        }
        _selectID = -1;
        _selectCoin.Clear();
    }

    private void DestroyCoin(List<Coin> coins)
    {
        foreach (var coinItem in coins)
        {
            Destroy(coinItem.gameObject);
        }

        CoinSpawn(coins.Count - _spawnCoinCount);
        _spawnCoinCount = 0;
    }
}
