using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂̏󋵂⏈�����Ǘ�����}�l�[�W���[
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // �J�E���g�_�E���A�^�C�}�[��X�R�A�̍X�V�p��Action
    public Action<int> OnGameStartCountUpdate;
    public Action<int> OnScoreUpdate;
    public Action<int> OnTimeUpdate;
    public Action<int> OnResultScore;
    public Action<int> OnHighScore;

    public GameObject CoinPrefab;
    public LineRenderer lineRenderer;
    public int CoinDestroyCount = 3;
    public float CoinConnectRange = 1.5f;

    // �Q�[���X�^�[�g�t���O
    public bool Startflag = false;

    [SerializeField]
    private int _score;
    [SerializeField]
    private float _time;
    // �e��ʂ�CanvasGroup
    [SerializeField]
    private CanvasGroup _titleGroup;
    [SerializeField]
    private CanvasGroup _inGameGroup;
    [SerializeField]
    private CanvasGroup _resultGroup;

    private float _maxtime;
    private bool _timeflag = true;
    private List<Coin> _selectCoin = new List<Coin>();
    private int _selectID = -1;
    private Coin.CoinName _selectCoinName;
    private bool _spawnCoinCountflag = false;
    // WaitForSeconds�̃L���b�V��
    private WaitForSeconds cachedWait = new WaitForSeconds(1.0f);
    // ���݂̏�Ԃɉ������������s���R���|�[�l���g
    private StateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();

        _stateMachine = GetComponent<StateMachine>();
    }

    private void Start()
    {
        // �^�C����ۑ�(���g���C�Ŏ��Ԃ�߂��ۂɎg�p)
        _maxtime = _time;
        // ��Ԃɉ�������ʂ�\��(�J�n���̓^�C�g�����)
        ChangeViewCanvasGroup(_stateMachine.titleState);
    }

    private void Update()
    {
        LineRendererUpdate();

        if (Startflag)
        {
            if (_timeflag)
            {
                TimeCount();
            }
        }
    }

    /// <summary>
    /// �Q�[���Ŏg�p����R�C���̐���
    /// </summary>
    /// <param name="count">��������R�C���̖���</param>
    public void CoinSpawn(int count)
    {
        int startX = -2;
        int startY = 5;
        int maxX = 5;
        int x = 0, y = 0;

        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(startX + x, startY + y);
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

    /// <summary>
    /// �R�C���̏ォ��}�E�X�����������̓���
    /// </summary>
    /// <param name="coin">�����ꂽ�R�C��</param>
    public void CoinDown(Coin coin)
    {
        if (!Startflag) return;
        _selectCoin.Add(coin);
        coin.SetIsSelect(true);

        _selectID = coin.ID;
        _selectCoinName = (Coin.CoinName)coin.ID;
        AudioManager.Instance.PlaySE(AudioManager.SEName.MouseDown);
    }

    /// <summary>
    /// �}�E�X���N���b�N������ԂŃ}�E�X���R�C���ɏd�˂����̓���
    /// </summary>
    /// <param name="coin">�}�E�X�ŏd�˂��R�C��</param>
    public void CoinEnter(Coin coin)
    {
        if (_selectID != coin.ID || !Startflag) return;

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

    /// <summary>
    /// �}�E�X�𗣂������̓���
    /// </summary>
    public void CoinUp()
    {
        if (!Startflag) return;

        if (_selectCoin.Count >= CoinDestroyCount)
        {
            NextCoinSpawn();
            DestroyCoin(_selectCoin);
        }
        else
        {
            // ����ȏ�����
            // 5�~�ʂ�2���A50�~�ʂ�2���ł�������(����)
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

    /// <summary>
    /// �C���Q�[������̃X���[�J�E���g�^�C�}�[
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartTimer()
    {
        if (OnTimeUpdate != null)
        {
            OnTimeUpdate.Invoke((int)Mathf.Floor(_time));
        }

        for (int i = 3 ;i >= 0; i-- )
        {
            if (i == 0)
            {
                Startflag = true;
                AudioManager.Instance.PlayBGM(AudioManager.BGMName.GamePlay);
            }

            if (OnGameStartCountUpdate != null)
            {
                OnGameStartCountUpdate.Invoke(i);
            }
            yield return cachedWait;
        }
    }

    /// <summary>
    /// �Q�[���J�n
    /// </summary>
    public void GameStart()
    {
        ChangeGameState(_stateMachine.inGameState);
        StartCoroutine("StartTimer");
    }

    /// <summary>
    /// �Q�[���̃��g���C
    /// </summary>
    public void GameRestart()
    {
        Startflag = false;
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
        if (OnHighScore != null)
        {
            int highscore = PlayerPrefs.GetInt("moneytsumscore", 0);
            OnHighScore.Invoke(highscore);
        }
        GameStart();
    }

    /// <summary>
    /// �Q�[����Ԃ̍X�V
    /// </summary>
    /// <param name="nextState">���̏��</param>
    public void ChangeGameState(IState nextState)
    {
        if (nextState == _stateMachine.currentState)
        {
            Debug.Log("�����X�e�[�g�ł�");
            return;
        }
        _stateMachine.ChangeState(nextState);
        ChangeViewCanvasGroup(nextState);
    }

    /// <summary>
    /// �I�����Ă���R�C����2���ȏ�̎��ALineRenderer��ݒ�
    /// </summary>
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

    /// <summary>
    /// �L�����o�X�O���[�v��ύX
    /// </summary>
    /// <param name="state">���̏��</param>
    private void ChangeViewCanvasGroup(IState state)
    {
        switch (state)
        {
            case TitleState:
                _titleGroup.alpha = 1;
                _inGameGroup.alpha = 0;
                _resultGroup.alpha = 0;
                break;
            case InGameState:
                _titleGroup.alpha = 0;
                _titleGroup.interactable = false;
                _inGameGroup.alpha = 1;
                _resultGroup.alpha = 0;
                break;
            case ResultState:
                _titleGroup.alpha = 0;
                _inGameGroup.alpha = 1;
                _resultGroup.alpha = 1;
                break;
        }
    }

    /// <summary>
    /// ���ݑI�����Ă���R�C���̎��̃����N(1�~->5�~�A50�~->100�~�Ȃ�)�̃R�C���𐶐�
    /// </summary>
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
                    GameObject coin = ObjectPool.Instance.GetGameObject(CoinPrefab, _selectCoin[_selectCoin.Count - 1].transform.position);
                    coin.GetComponent<Coin>().SetCoin(_selectID + 1);
                    _spawnCoinCountflag = true;
                }
                break;
            case Coin.CoinName.Five:
            case Coin.CoinName.Fifty:
                if (_selectCoin.Count >= 2)
                {
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

    /// <summary>
    /// �R�C���������̓���
    /// </summary>
    /// <param name="coins">��������R�C���̃��X�g</param>
    private void DestroyCoin(List<Coin> coins)
    {
        foreach (var coinItem in coins)
        {
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

    /// <summary>
    /// �X�R�A�J�E���g�X�V
    /// </summary>
    /// <param name="score">�X�R�A</param>
    private void ScoreCount(int score)
    {
        _score += score;

        if (OnScoreUpdate != null)
        {
            OnScoreUpdate.Invoke(_score);
        }
    }
    
    /// <summary>
    /// �^�C�}�[�J�E���g�X�V
    /// </summary>
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
            Startflag = false;

            ChangeGameState(_stateMachine.resultState);

            if (OnResultScore != null)
            {
                OnResultScore.Invoke(_score);
            }
        }
    }
}
