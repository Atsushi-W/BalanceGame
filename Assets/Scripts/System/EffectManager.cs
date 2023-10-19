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
    private GameObject _getscore;
    [SerializeField]
    private GameObject _scoreObjects;

    private Dictionary<int, List<ParticleSystem>> pooledEffectObjects = new Dictionary<int, List<ParticleSystem>>();
    private Dictionary<int, List<DisplayScore>> pooledScoreObjects = new Dictionary<int, List<DisplayScore>>();

    /// <summary>
    /// �G�t�F�N�g�̐���
    /// </summary>
    /// <param name="pos">�G�t�F�N�g�̃|�W�V����</param>
    /// <returns></returns>
    public ParticleSystem GetEffectGameObject(Vector3 pos)
    {
        int key = EffectPrefab.GetInstanceID();

        // Dictionary��key�����݂��Ȃ���΍쐬
        if (pooledEffectObjects.ContainsKey(key) == false)
        {
            pooledEffectObjects.Add(key, new List<ParticleSystem>());
        }

        List<ParticleSystem> effectsObjects = pooledEffectObjects[key];
        ParticleSystem effect = null;

        for (int i = 0; i < effectsObjects.Count; i++)
        {
            effect = effectsObjects[i];

            // ���ݔ�A�N�e�B�u�i���g�p�j�ł����
            if (effect.gameObject.activeInHierarchy == false)
            {
                // �ʒu�Ɗp�x��ݒ肵Active�ɂ���
                effect.transform.position = pos;
                effect.transform.rotation = Quaternion.identity;
                effect.gameObject.SetActive(true);

                return effect;
            }
        }

        // �g�p�ł�����̂��Ȃ��ꍇ�͐�������
        GameObject effectObject = Instantiate(EffectPrefab, pos, Quaternion.identity);
        effect = effectObject.GetComponent<ParticleSystem>();
        effect.name = EffectPrefab.name;
        effect.transform.parent = transform;
        effectsObjects.Add(effect);

        return effect;
    }

    /// <summary>
    /// �X�R�A�\���̐���
    /// </summary>
    /// <param name="pos">�X�R�A(���z)��\������|�W�V����</param>
    /// <returns></returns>
    public DisplayScore GetScoreObject(Vector3 pos, int scorenum)
    {
        int key = _getscore.GetInstanceID();

        // Dictionary��key�����݂��Ȃ���΍쐬
        if (pooledScoreObjects.ContainsKey(key) == false)
        {
            pooledScoreObjects.Add(key, new List<DisplayScore>());
        }

        List<DisplayScore> scoreObjects = pooledScoreObjects[key];
        DisplayScore score = null;

        // �|�W�V������Transform����RectTransform�֕ϊ�
        Vector2 newPos = Vector2.zero;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPos, Camera.main, out newPos);

        for (int i = 0; i < scoreObjects.Count; i++)
        {
            score = scoreObjects[i];

            // ���ݔ�A�N�e�B�u�i���g�p�j�ł����
            if (score.gameObject.activeInHierarchy == false)
            {
                // �ʒu��ݒ肵Active�ɂ���
                score.transform.localPosition = newPos;
                score.gameObject.SetActive(true);
                score.SetScoreDesplay(scorenum);
                return score;
            }
        }

        // �g�p�ł�����̂��Ȃ��ꍇ�͐�������
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
