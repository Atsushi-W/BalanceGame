using UnityEngine;

/// <summary>
/// �V���O���g���̋@�\�����R���|�[�l���g�B�p���ł̎g�p�̂�
/// </summary>
/// <typeparam name="T">�V���O���g���Ƃ���R���|�[�l���g</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError(typeof(T) + "���V�[���ɑ��݂��܂���B");
                }
            }

            return instance;
        }
    }

    private static T instance;

    protected virtual void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
}