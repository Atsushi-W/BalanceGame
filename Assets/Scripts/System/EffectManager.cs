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
    private GameObject _getscore;
    [SerializeField]
    private GameObject _scoreObjects;

    private Dictionary<int, List<ParticleSystem>> pooledEffectObjects = new Dictionary<int, List<ParticleSystem>>();
    private Dictionary<int, List<DisplayScore>> pooledScoreObjects = new Dictionary<int, List<DisplayScore>>();

    /// <summary>
    /// エフェクトの生成
    /// </summary>
    /// <param name="pos">エフェクトのポジション</param>
    /// <returns></returns>
    public ParticleSystem GetEffectGameObject(Vector3 pos)
    {
        int key = EffectPrefab.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成
        if (pooledEffectObjects.ContainsKey(key) == false)
        {
            pooledEffectObjects.Add(key, new List<ParticleSystem>());
        }

        List<ParticleSystem> effectsObjects = pooledEffectObjects[key];
        ParticleSystem effect = null;

        for (int i = 0; i < effectsObjects.Count; i++)
        {
            effect = effectsObjects[i];

            // 現在非アクティブ（未使用）であれば
            if (effect.gameObject.activeInHierarchy == false)
            {
                // 位置と角度を設定しActiveにする
                effect.transform.position = pos;
                effect.transform.rotation = Quaternion.identity;
                effect.gameObject.SetActive(true);

                return effect;
            }
        }

        // 使用できるものがない場合は生成する
        GameObject effectObject = Instantiate(EffectPrefab, pos, Quaternion.identity);
        effect = effectObject.GetComponent<ParticleSystem>();
        effect.name = EffectPrefab.name;
        effect.transform.parent = transform;
        effectsObjects.Add(effect);

        return effect;
    }

    /// <summary>
    /// スコア表示の生成
    /// </summary>
    /// <param name="pos">スコア(金額)を表示するポジション</param>
    /// <returns></returns>
    public DisplayScore GetScoreObject(Vector3 pos, int scorenum)
    {
        int key = _getscore.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成
        if (pooledScoreObjects.ContainsKey(key) == false)
        {
            pooledScoreObjects.Add(key, new List<DisplayScore>());
        }

        List<DisplayScore> scoreObjects = pooledScoreObjects[key];
        DisplayScore score = null;

        // ポジションをTransformからRectTransformへ変換
        Vector2 newPos = Vector2.zero;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPos, Camera.main, out newPos);

        for (int i = 0; i < scoreObjects.Count; i++)
        {
            score = scoreObjects[i];

            // 現在非アクティブ（未使用）であれば
            if (score.gameObject.activeInHierarchy == false)
            {
                // 位置を設定しActiveにする
                score.transform.localPosition = newPos;
                score.gameObject.SetActive(true);
                score.SetScoreDesplay(scorenum);
                return score;
            }
        }

        // 使用できるものがない場合は生成する
        GameObject getscore = Instantiate(_getscore, _canvas);
        getscore.name = _getscore.name;
        score = getscore.GetComponent<DisplayScore>();
        score.transform.localPosition = newPos;
        score.transform.SetParent(_scoreObjects.transform);
        score.SetScoreDesplay(scorenum);
        scoreObjects.Add(score);

        return score;
    }
}
