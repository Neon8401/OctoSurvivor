using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static Charadata;
using static Lvdata;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    /// <summary>
    /// アイテムの種類定義
    /// </summary>
    public enum ItemType
    {
        EXP,
        Wood, // 木
        Stone, // 石
        ThrowAxe, // 投げオノ（木と石で作る！）
        Kinoko, // きのこ
        Meat,
        Clear
    }

    [SerializeField] private ItemType type;
    [SerializeField] private Charadata _charadata;
    [SerializeField] private Lvdata _exp;
    MobStatus _status;
    [SerializeField] MainSceneController _mainSceneController;
    [SerializeField] TimeManager _timeManager;
    /// <summary>
    /// 初期化処理
    /// </summary
    /// 
    void Start()
    {
        _status = GetComponent<MobStatus>();
    }
    public void Initialize()
    {
        // アニメーションが終わるまでcolliderを無効に
        var colliderCache = GetComponent<Collider>();
        colliderCache.enabled = false;

        // 出現アニメーション
        var transformCache = transform;
        var dropPosition = transformCache.localPosition +
                           new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        transformCache.DOLocalMove(dropPosition, 0.5f);
        var defaultScale = transformCache.localScale;
        transformCache.localScale = Vector3.zero;
        transformCache.DOScale(defaultScale, 0.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                // アニメーションが終わったらcolliderを有効に
                colliderCache.enabled = true;
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // アイテムをプレイヤーの子オブジェクトにすることで、取得アニメーション中にプレイヤーが移動しても追尾してくれる
        transform.parent = other.transform;
        // プレイヤーの所持品として追加
        if (type == ItemType.EXP)
        {
            _charadata.EXP += 10;
            var exp = _exp.playerExpTable[_charadata.LV];
            Debug.Log("OK3");
            if (_charadata.EXP >= exp.exp)
            {
                AudioManager.instance.PlaySE("LvUp");
                _charadata.LV += 1;
                _charadata.ATK += 1;
                _charadata.MAXHP += 10;
                //EditorUtility.SetDirty(_charadata);
            }
            Destroy(gameObject);
        }
        if (type == ItemType.Meat)
        {
            Debug.Log("OK4");
            _charadata.HP += 10;
            //EditorUtility.SetDirty(_charadata);

            AudioManager.instance.PlaySE("Heal");
            Destroy(gameObject);
            
        }
        if(type == ItemType.Clear)
        {
            _mainSceneController.SaveCharaData();
            AudioManager.instance.StopBGM();
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

            GameClearCon.Score = _timeManager.Score + 100000;
            SceneManager.LoadScene("GameClearScene");
        }
        else
        {
            OwnedItemsData.Instance.Add(type);
            OwnedItemsData.Instance.Save();
        }

        AudioManager.instance.PlaySE("PickUp");
        GetComponent<Renderer>().enabled = false;

        // オブジェクトの破棄
        Destroy(gameObject);

    }
}