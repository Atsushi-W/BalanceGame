using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM,SEを管理、再生するマネージャー
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // SE一覧
    public enum SEName
    {
        MouseDown,
        MouseEnter,
        DestroyCoin,
        RemoveCoin,
        Result,
        StartTimer,
        GameStart,
        GameEnd,
    }

    // BGM一覧
    public enum BGMName
    {
        Title,
        GamePlay,
    }

    [SerializeField, Range(0.0f, 1.0f)]
    private float _bgmVolume = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    private float _seVolume = 1.0f;
    [SerializeField]
    List<AudioClip> _seList = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> _bgmList = new List<AudioClip>();

    private AudioSource _bgmAudioSource;
    private List<AudioSource> _seAudioSourceList = new List<AudioSource>();

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < _seList.Count; i++)
        {
            AudioSource source = this.transform.gameObject.AddComponent<AudioSource>();
            source.clip = _seList[i];
            source.volume = _seVolume;
            source.playOnAwake = false;
            _seAudioSourceList.Add(source);
        }

        _bgmAudioSource = this.transform.gameObject.AddComponent<AudioSource>();
        _bgmAudioSource.volume = _bgmVolume;
        _bgmAudioSource.playOnAwake = false;
        _bgmAudioSource.loop = true;
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="sename">SE名</param>
    public void PlaySE(SEName sename)
    {
        AudioSource se = Instance._seAudioSourceList.FirstOrDefault(x => x.clip.name == sename.ToString());

        if (se != null)
        {
            se.Play();
        }
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="bgmname">BGM名</param>
    public void PlayBGM(BGMName bgmname)
    {
        AudioClip bgm = Instance._bgmList.FirstOrDefault(x => x.name == bgmname.ToString());

        if (bgm != null)
        {
            _bgmAudioSource.clip = bgm;
            _bgmAudioSource.Play();
        }
    }


    /// <summary>
    /// BGMの停止
    /// </summary>
    public void StopBGM()
    {
        _bgmAudioSource.Stop();
    }

}
