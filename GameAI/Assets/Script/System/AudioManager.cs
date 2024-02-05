using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // ���
    [System.Serializable] struct AudioInfo
    {
        public string name;
        public AudioClip sounds;
    };

    //  ���ʉ����
    [SerializeField] List<AudioInfo> m_SEInfo;
    //  BGM���
    [SerializeField] List<AudioInfo> m_BGMInfo;

    //  ���ʉ�
    private Dictionary<string, AudioClip> m_se = new Dictionary<string, AudioClip>();
    //  BGM
    private Dictionary<string, AudioClip> m_bgm = new Dictionary<string, AudioClip>();

    //  �����p
    private AudioSource BGM = null;
    private AudioSource SE = null;

    //  �N����
    private void Awake()
    {
        //  �C���X�^���X����
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

        //  ���ǉ�
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
    
    //  SE�ǉ�
    public void PlaySE(string SEName)
    {
        SE.PlayOneShot(m_se[SEName]);
    }

    //  BGM�ǉ�
    public void PlayBGM(string BGMName)
    {
        BGM.clip = m_bgm[BGMName];
        BGM.Play();
    }

    //  BGM��~
    public void StopBGM()
    {
        BGM.Stop();
    }
}
