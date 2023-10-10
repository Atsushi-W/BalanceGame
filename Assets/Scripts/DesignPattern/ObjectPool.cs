using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R�C���̐������Ǘ�����I�u�W�F�N�g�v�[��
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
        // �e�I�u�W�F�N�g�� Transform ���擾����
        Transform parentTransform = transform;

        // �q�I�u�W�F�N�g��S�Ď擾����
        foreach (Transform child in parentTransform)
        {
            child.gameObject.GetComponent<Coin>().SetIsSelect(false);
            ReleaseGameObject(child.gameObject);
        }
    }
}