using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SEName
    {
        MouseDown,
        MouseEnter,
        DestroyCoin,
        RemoveCoin,
        Result,
    }

    public static AudioManager Instance { get; private set; }

    [SerializeField, Range(0.0f, 1.0f)]
    private float _seVolume = 1.0f;
    [SerializeField]
    List<AudioClip> _seList = new List<AudioClip>();

    private List<AudioSource> _seAudioSourceList = new List<AudioSource>();

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < _seList.Count; i++)
        {
            AudioSource source = this.transform.gameObject.AddComponent<AudioSource>();
            source.clip = _seList[i];
            source.volume = _seVolume;
            source.playOnAwake = false;
            _seAudioSourceList.Add(source);
        }
    }

    public void PlaySE(SEName sename)
    {
        AudioSource se = Instance._seAudioSourceList.FirstOrDefault(x => x.clip.name == sename.ToString());

        if (se != null)
        {
            se.Play();
        }
    }

}
