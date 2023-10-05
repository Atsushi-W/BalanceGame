using UnityEngine;

/// <summary>
/// シングルトンの機能を持つコンポーネント。継承での使用のみ
/// </summary>
/// <typeparam name="T">シングルトンとするコンポーネント</typeparam>
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
                    Debug.LogError(typeof(T) + "がシーンに存在しません。");
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