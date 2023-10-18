using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �G�t�F�N�g�̐������Ǘ�����}�l�[�W���[
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
    /// �G�t�F�N�g�̐���
    /// </summary>
    /// <param name="pos">�G�t�F�N�g�̃|�W�V�����v</param>
    /// <returns></returns>
    public GameObject GetEffectGameObject(Vector3 pos)
    {
        int key = EffectPrefab.GetInstanceID();

        // Dictionary��key�����݂��Ȃ���΍쐬
        if (pooledEffectObjects.ContainsKey(key) == false)
        {
            pooledEffectObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> effectsObjects = pooledEffectObjects[key];
        GameObject effect = null;

        for (int i = 0; i < effectsObjects.Count; i++)
        {
            effect = effectsObjects[i];

            // ���ݔ�A�N�e�B�u�i���g�p�j�ł����
            if (effect.activeInHierarchy == false)
            {
                // �ʒu�Ɗp�x��ݒ肵Active�ɂ���
                effect.transform.position = pos;
                effect.transform.rotation = Quaternion.identity;
                effect.SetActive(true);

                SetGetScore(pos);

                return effect;
            }
        }

        // �g�p�ł�����̂��Ȃ��ꍇ�͐�������
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
