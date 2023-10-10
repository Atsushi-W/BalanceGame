using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル画面での振る舞いを行うStateクラス
/// </summary>
public class TitleState : IState
{
    public void Enter()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BGMName.Title);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
