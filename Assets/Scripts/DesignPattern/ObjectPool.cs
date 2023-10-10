using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コインの生成を管理するオブジェクトプール
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    private Dictionary<string, Queue<GameObject>> _pooledGameObjects = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetGameObject(GameObject prefab, Vector2 pos)
    {
        string key = prefab.name;

        if (_pooledGameObjects.ContainsKey(key) == false)
        {
            _pooledGameObjects.Add(key, new Queue<GameObject>());
        }

        Queue<GameObject> gameObjects = _pooledGameObjects[key];
        GameObject go = null;

        if (gameObjects.Count > 0)
        {
            go = gameObjects.Dequeue();
            go.transform.position = pos;
            go.SetActive(true);
        }
        else
        {
            go = Instantiate(prefab, pos, Quaternion.identity);
            go.name = prefab.name;
            go.transform.parent = transform;
        }
        return go;
    }

    public void ReleaseGameObject(GameObject go)
    {
        go.SetActive(false);

        string key = go.name;
        Queue<GameObject> gameObjects = _pooledGameObjects[key];
        gameObjects.Enqueue(go);
    }

    public void AllReleaseGameObject()
    {
        // 親オブジェクトの Transform を取得する
        Transform parentTransform = transform;

        // 子オブジェクトを全て取得する
        foreach (Transform child in parentTransform)
        {
            child.gameObject.GetComponent<Coin>().SetIsSelect(false);
            ReleaseGameObject(child.gameObject);
        }
    }
}