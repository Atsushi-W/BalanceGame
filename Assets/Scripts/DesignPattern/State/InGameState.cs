using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �C���Q�[����ʂł̐U�镑�����s��State�N���X
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
