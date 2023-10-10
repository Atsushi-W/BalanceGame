using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲーム画面での振る舞いを行うStateクラス
/// </summary>
public class InGameState : IState
{
    public void Enter()
    {
        AudioManager.Instance.StopBGM();
        GameManager.Instance.CoinSpawn(40);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
