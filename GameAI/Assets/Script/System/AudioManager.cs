using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // 情報
    [System.Serializable] struct AudioInfo
    {
        public string name;
        public AudioClip sounds;
    };

    //  効果音情報
    [SerializeField] List<AudioInfo> m_SEInfo;
    //  BGM情報
    [SerializeField] List<AudioInfo> m_BGMInfo;

    //  効果音
    private Dictionary<string, AudioClip> m_se = new Dictionary<string, AudioClip>();
    //  BGM
    private Dictionary<string, AudioClip> m_bgm = new Dictionary<string, AudioClip>();

    //  音声用
    private AudioSource BGM = null;
    private AudioSource SE = null;

    //  起動時
    private void Awake()
    {
        //  インスタンス生成
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            var a = gameObject.GetComponents<AudioSource>();
            SE = a[0];
            BGM = a[1];
            BGM.loop = true;
            BGM.volume = 0.05f;
        }
        else
        {
            Destroy(gameObject);
        }

        //  音追加
        foreach (var se in m_SEInfo)
        {
            Debug.Log(se.name);
            Debug.Log(se.sounds);
            m_se.Add(se.name, se.sounds);
        }
        foreach (var bgm in m_BGMInfo)
        {
            Debug.Log(bgm.name);
            Debug.Log(bgm.sounds);
            m_bgm.Add(bgm.name, bgm.sounds);
        }
    }
    
    //  SE追加
    public void PlaySE(string SEName)
    {
        SE.PlayOneShot(m_se[SEName]);
    }

    //  BGM追加
    public void PlayBGM(string BGMName)
    {
        BGM.clip = m_bgm[BGMName];
        BGM.Play();
    }

    //  BGM停止
    public void StopBGM()
    {
        BGM.Stop();
    }
}
