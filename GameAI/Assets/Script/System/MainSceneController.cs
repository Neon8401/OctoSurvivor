using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using static Charadata;
using UnityEditor;

public class MainSceneController : SingletonMonoBehaviourInSceneBase<MainSceneController>
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] TimeManager _timeManager;
    public const int IncrementMinutesPerAttack = 10;
    public const int IncrementMinutesPerCreateItem = 5;

    [SerializeField] Text _bulletCount;
    [SerializeField] ShotBullet _shotBullet;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] PlayerController _playerController;
    [SerializeField] Charadata _charadata;
    [SerializeField] Charadata _charadata1;

    LvUI _lvui;
    private bool _isGameOver;

    void Start()
    {
        LoadCharaData();
        LoadEnemyData();
        AudioManager.instance.PlayBGM("Stage");
        // 時間経過処理
        _bulletCount.text = "" +_shotBullet.shotCount.ToString() +"/5";

        SetUp().Forget();
    }

    public async UniTaskVoid SetUp()
    {
        Debug.Log("OK");
        _startPanel.SetActive(true);
        await UniTask.WaitForSeconds(2);
        _startPanel.SetActive(false);
        _timeManager.SetUp();
        _playerController.PlayerStart();

    }

    private void Update()
    {
        _bulletCount.text = "" + _shotBullet.shotCount.ToString() + "/5";
    }

    public void LoadCharaData()
    {
        _charadata.MAXHP = SaveData.Instance.MAXHP;
        _charadata.HP = SaveData.Instance.HP;
        _charadata.ATK = SaveData.Instance.ATK;
        _charadata.EXP = SaveData.Instance.EXP;
        _charadata.LV = SaveData.Instance.LV;
        _charadata.GETEXP = SaveData.Instance.GETEXP;
    }

    public void LoadEnemyData()
    {
        Debug.Log("OKKK");
        _charadata1.MAXHP = SaveData1.Instance.MAXHP;
        _charadata1.HP = SaveData1.Instance.HP;
        _charadata1.ATK = SaveData1.Instance.ATK;
        _charadata1.EXP = SaveData1.Instance.EXP;
        _charadata1.LV = SaveData1.Instance.LV;
        _charadata1.GETEXP = SaveData1.Instance.GETEXP;
    }
    public void SaveCharaData()
    {
        SaveData.Instance.MAXHP =_charadata.MAXHP;
        SaveData.Instance.HP = _charadata.MAXHP;
        SaveData.Instance.ATK = _charadata.ATK;
        SaveData.Instance.EXP = _charadata.EXP;
        SaveData.Instance.LV = _charadata.LV;
        SaveData.Instance.GETEXP = _charadata.GETEXP;
        SaveData.Instance.Save();
    }


    public void GameOver()
    {
        SaveCharaData();
        AudioManager.instance.StopBGM();
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        _isGameOver = true;
        _playerController.PlayerStart();
        DOVirtual.DelayedCall(3, () =>
        {
            GameOverSceneController.Score = _timeManager.Score;
            SceneManager.LoadScene("GameOverScene");
        });
    }

    private IEnumerator TimerLoop()
    {
        while (!_isGameOver)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

}