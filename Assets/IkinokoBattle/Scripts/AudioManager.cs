using System;
using System.Collections.Generic;
using UnityEngine;

// Audioの管理をするクラス シーンを跨いでも破壊されないようにシングルトンで実装する
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [SerializeField] private AudioSource _audioSource;
    // readonly: 初期化以降は変更できない
    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get
        {return _instance;}
    }

    private void Awake()
    {
        if(null != _instance) {
            Destroy(gameObject);
            return;
        }
        // シーンを遷移しても壊されないようにする
        DontDestroyOnLoad(gameObject);
        _instance = this;

        // Resources/2D_SEフォルダ下の全てのAudioClipを取得する
        var audioClip = Resources.LoadAll<AudioClip>("2D_SE");
        foreach (var clip in audioClip)
        {
            // Dictionaryに追加する
            _clips.Add(clip.name, clip);
        }
    }

    public void Play(string clipName)
    {
        if(!_clips.ContainsKey(clipName)) throw new Exception("Sound " + clipName +  "is not found");
        _audioSource.clip = _clips[clipName];
        _audioSource.Play();
    }
}
