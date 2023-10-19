using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private ParticleSystem[] _particles;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        // 子オブジェクト取得して格納
        int childCount = transform.childCount;
        _particles = new ParticleSystem[childCount];
        for(int i = 0;i < childCount;i++)
        {
            Transform childTransform = transform.GetChild(i);
            _particles[i] = childTransform.gameObject.GetComponent<ParticleSystem>();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].gameObject.SetActive(true);
        }
    }
}
