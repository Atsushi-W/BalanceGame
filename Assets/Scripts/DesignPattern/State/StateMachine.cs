using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���̏��(State)���Ǘ�����R���|�[�l���g
/// </summary>
public class StateMachine : MonoBehaviour
{
    /// <summary>���݂̃Q�[�����</summary>
    public IState currentState;
    /// <summary>�^�C�g����ʂ̏��</summary>
    public TitleState titleState;
    /// <summary>�C���Q�[����ʂ̏��</summary>
    public InGameState inGameState;
    /// <summary>���U���g��ʂ̏��</summary>
    public ResultState resultState;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        // ���������̓^�C�g����Ԃ�
        ChangeState(titleState);
    }

    /// <summary>
    /// ��Ԃ̍X�V
    /// </summary>
    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    /// <summary>
    /// ��Ԃ�ύX����
    /// </summary>
    /// <param name="nextState">���̏��(State)</param>
    public void ChangeState(IState nextState)
    {
        if (currentState != null)
        {
            // ���݂̏�Ԃ̏I�����������s
            currentState.Exit();
            currentState = nextState;
        }

        // ���̏�Ԃ̊J�n���������s
        nextState.Enter();
    }

    /// <summary>
    /// �eState�̏�����
    /// </summary>
    private void Initialize()
    {
        titleState = new TitleState();
        inGameState = new InGameState();
        resultState = new ResultState();
    }
}
