using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���U���g��ʂł̐U�镑�����s��State�N���X
/// </summary>
public class ResultState : IState
{
    public void Enter()
    {
        AudioManager.Instance.PlaySE(AudioManager.SEName.GameEnd);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
