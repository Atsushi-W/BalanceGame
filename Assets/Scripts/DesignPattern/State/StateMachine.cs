using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの状態(State)を管理するコンポーネント
/// </summary>
public class StateMachine : MonoBehaviour
{
    /// <summary>現在のゲーム状態</summary>
    public IState currentState;
    /// <summary>タイトル画面の状態</summary>
    public TitleState titleState;
    /// <summary>インゲーム画面の状態</summary>
    public InGameState inGameState;
    /// <summary>リザルト画面の状態</summary>
    public ResultState resultState;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        // 初期化時はタイトル状態へ
        ChangeState(titleState);
    }

    /// <summary>
    /// 状態の更新
    /// </summary>
    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    /// <summary>
    /// 状態を変更する
    /// </summary>
    /// <param name="nextState">次の状態(State)</param>
    public void ChangeState(IState nextState)
    {
        if (currentState != null)
        {
            // 現在の状態の終了処理を実行
            currentState.Exit();
            currentState = nextState;
        }

        // 次の状態の開始処理を実行
        nextState.Enter();
    }

    /// <summary>
    /// 各Stateの初期化
    /// </summary>
    private void Initialize()
    {
        titleState = new TitleState();
        inGameState = new InGameState();
        resultState = new ResultState();
    }
}
