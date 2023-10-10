using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト画面での振る舞いを行うStateクラス
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
