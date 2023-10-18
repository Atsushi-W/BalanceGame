using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// エフェクトの生成を管理するマネージャー
/// </summary>
public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [SerializeField]
    private GameObject EffectPrefab;
    [SerializeField]
    private RectTransform _canvas;
    [SerializeField]
    private RectTransform _getscore;


    private Dictionary<int, List<GameObject>> pooledEffectObjects = new Dictionary<int, List<GameObject>>();

    /// <summary>
    /// エフェクトの生成
    /// </summary>
    /// <param name="pos">エフェクトのポジション」</param>
    /// <returns></returns>
    public GameObject GetEffectGameObject(Vector3 pos)
    {
        int key = EffectPrefab.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成
        if (pooledEffectObjects.ContainsKey(key) == false)
        {
            pooledEffectObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> effectsObjects = pooledEffectObjects[key];
        GameObject effect = null;

        for (int i = 0; i < effectsObjects.Count; i++)
        {
            effect = effectsObjects[i];

            // 現在非アクティブ（未使用）であれば
            if (effect.activeInHierarchy == false)
            {
                // 位置と角度を設定しActiveにする
                effect.transform.position = pos;
                effect.transform.rotation = Quaternion.identity;
                effect.SetActive(true);

                SetGetScore(pos);

                return effect;
            }
        }

        // 使用できるものがない場合は生成する
        effect = Instantiate(EffectPrefab, pos, Quaternion.identity);
        effect.transform.parent = transform;
        effectsObjects.Add(effect);

        SetGetScore(pos);

        return effect;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    private void SetGetScore(Vector3 pos)
    {
        Vector2 newPos = Vector2.zero;

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPos, Camera.main, out newPos);
        _getscore.localPosition = newPos;
    }
}
