using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public Action<int> OnGameStartCountUpdate;
    public Action<int> OnScoreUpdate;
    public Action<int> OnTimeUpdate;
    public Action<int> OnResultScore;

    public GameObject CoinPrefab;

    public LineRenderer lineRenderer;

    public int CoinDestroyCount = 3;
    public float CoinConnectRange = 1.5f;

    private List<Coin> _selectCoin = new List<Coin>();
    private int _selectID = -1;
    private Coin.CoinName _selectCoinName;
    private bool _spawnCoinCountflag = false;
    [SerializeField]
    private int _score;
    [SerializeField]
    private float _time;
    private float _maxtime;
    private bool _timeflag = true;

    private bool _startflag = false;

    // WaitForSecondsƒLƒƒƒbƒVƒ…
    private WaitForSeconds cachedWait = new WaitForSeconds(1.0f);

    [SerializeField]
    private CanvasGroup _titleGroup;
    [SerializeField]
    private CanvasGroup _inGameGroup;
    [SerializeField]
    private CanvasGroup _resultGroup;

    private void Start()
    {
        _maxtime = _time;
    }

    void Update()
    {
        LineRendererUpdate();

        if (_startflag)
        {
            if (_timeflag)
            {
                TimeCount();
            }
        }
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
            //GameObject coin = Instantiate(CoinPrefab, pos, Quaternion.identity);
            GameObject coin = ObjectPool.Instance.GetGameObject(CoinPrefab, pos);
            coin.GetComponent<Coin>().SetCoin((Coin.CoinName)UnityEngine.Random.Range(0, 5));
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
            case Coin.CoinName.Ten:
            case Coin.CoinName.Hundred:
                if (_selectCoin.Count >= 5)
                {
                    //GameObject coin = Instantiate(CoinPrefab, _selectCoin[_selectCoin.Count - 1].transform.position, Quaternion.identity);
                    GameObject coin = ObjectPool.Instance.GetGameObject(CoinPrefab, _selectCoin[_selectCoin.Count - 1].transform.position);
                    coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                    _spawnCoinCountflag =true;
                }
                break;
            case Coin.CoinName.Five:
            case Coin.CoinName.Fifty:
                if (_selectCoin.Count >= 2)
                {
                    //GameObject coin = Instantiate(CoinPrefab, _selectCoin[_selectCoin.Count - 1].transform.position, Quaternion.identity);
                    GameObject coin = ObjectPool.Instance.GetGameObject(CoinPrefab, _selectCoin[_selectCoin.Count - 1].transform.position);
                    coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                    _spawnCoinCountflag = true;
                }
                break;
            case Coin.CoinName.FiveHundreds:
            default:
                break;
        }
    }

    public void CoinDown(Coin coin)
    {
        if (!_startflag) return;
        _selectCoin.Add(coin);
        coin.SetIsSelect(true);

        _selectID = coin.ID;
        _selectCoinName = (Coin.CoinName)coin.ID;
        AudioManager.Instance.PlaySE(AudioManager.SEName.MouseDown);
    }

    public void CoinEnter(Coin coin)
    {
        if (_selectID != coin.ID || !_startflag) return;

        if (coin.IsSelect)
        {
            if (_selectCoin.Count >= 2 && _selectCoin[_selectCoin.Count - 2] == coin)
            {
                Coin removeCoin = _selectCoin[_selectCoin.Count - 1];
                removeCoin.SetIsSelect(false);
                _selectCoin.Remove(removeCoin);
                AudioManager.Instance.PlaySE(AudioManager.SEName.RemoveCoin);
            }
        }
        else
        {
            float length = (_selectCoin[_selectCoin.Count - 1].transform.position - coin.transform.position).magnitude;
            if (length < CoinConnectRange)
            {
                _selectCoin.Add(coin);
                coin.SetIsSelect(true);
                AudioManager.Instance.PlaySE(AudioManager.SEName.MouseEnter);
            }
        }
    }

    public void CoinUp()
    {
        if (!_startflag) return;

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
            //Destroy(coinItem.gameObject);
            coinItem.SetIsSelect(false);
            ObjectPool.Instance.ReleaseGameObject(coinItem.gameObject);

            switch (_selectCoinName)
            {
                case Coin.CoinName.One:
                    ScoreCount(1);
                    break;
                case Coin.CoinName.Five:
                    ScoreCount(5);
                    break;
                case Coin.CoinName.Ten:
                    ScoreCount(10);
                    break;
                case Coin.CoinName.Fifty:
                    ScoreCount(50);
                    break;
                case Coin.CoinName.Hundred:
                    ScoreCount(100);
                    break;
                case Coin.CoinName.FiveHundreds:
                    ScoreCount(500);
                    break;
                default:
                    break;
            }
        }

        if (_spawnCoinCountflag)
        {
            CoinSpawn(coins.Count - 1);
            _spawnCoinCountflag = false;
        }
        else
        {
            CoinSpawn(coins.Count);
        }

        AudioManager.Instance.PlaySE(AudioManager.SEName.DestroyCoin);
    }

    private void ScoreCount(int score)
    {
        _score += score;

        if (OnScoreUpdate != null)
        {
            OnScoreUpdate.Invoke(_score);
        }
    }

    private void TimeCount()
    {
        _time -= Time.deltaTime;

        if (OnTimeUpdate != null)
        {
            OnTimeUpdate.Invoke((int)Mathf.Floor(_time));
        }

        if (_time <= 0)
        {
            _timeflag = false;
            _startflag = false;
            AudioManager.Instance.PlaySE(AudioManager.SEName.GameEnd);

            _titleGroup.alpha = 0;
            _inGameGroup.alpha = 1;
            _resultGroup.alpha = 1;
            if (OnResultScore != null)
            {
                OnResultScore.Invoke(_score);
            }
        }
    }

    IEnumerator StartTimer()
    {
        if (OnTimeUpdate != null)
        {
            OnTimeUpdate.Invoke((int)Mathf.Floor(_time));
        }

        for (int i = 3 ;i >= 0; i-- )
        {
            if (i == 0)
            {
                _startflag = true;
                AudioManager.Instance.PlayBGM(AudioManager.BGMName.GamePlay);
            }

            if (OnGameStartCountUpdate != null)
            {
                OnGameStartCountUpdate.Invoke(i);
            }
            yield return cachedWait;
        }
    }

    public void GameStart()
    {
        AudioManager.Instance.StopBGM();

        _titleGroup.alpha = 0;
        _titleGroup.interactable = false;
        _inGameGroup.alpha = 1;
        _resultGroup.alpha = 0;

        CoinSpawn(40);
        StartCoroutine("StartTimer");
    }

    public void GameRestart()
    {
        _startflag = false;
        _timeflag = true;
        _time = _maxtime;
        _selectID = -1;
        _selectCoin.Clear();
        ObjectPool.Instance.AllReleaseGameObject();
        _score = 0;
        if (OnScoreUpdate != null)
        {
            OnScoreUpdate.Invoke(_score);
        }
        GameStart();
    }
}
