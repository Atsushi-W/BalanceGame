using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�C�g����ʂł̐U�镑�����s��State�N���X
/// </summary>
public class TitleState : IState
{
    public void Enter()
    {
        // WebGL��ł͍ŏ��͉�����Ȃ�(��ʂ̃N���b�N���K�v)���ߍ폜
        //AudioManager.Instance.PlayBGM(AudioManager.BGMName.Title);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
