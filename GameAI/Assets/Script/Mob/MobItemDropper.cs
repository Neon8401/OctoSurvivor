using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MobStatus))]
public class MobItemDropper : MonoBehaviour
{
    private static string _lastKilledEnemyTag;
    private static int _chainKillCount;

    [SerializeField] [Range(0, 1)] private float dropRate = 0.1f; // アイテム出現確率
    [SerializeField] private bool needChainKill;
    [SerializeField] private int needChainKillCount;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private Item itemPrefab2;
    [SerializeField] private int number = 1; // アイテム出現個数

    private MobStatus _status;
    private bool _isDropInvoked;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    private void Update()
    {
        if (_status.Life <= 0)
        {
            // ライフが尽きた時に実行
            DropIfNeeded();
        }
    }

    /// <summary>
    /// 必要であればアイテムを出現させます。
    /// </summary>
    private void DropIfNeeded()
    {
        // 1つのインスタンスにつき1回だけ実行させるための制御
        if (_isDropInvoked) return;
        _isDropInvoked = true;

        if (_lastKilledEnemyTag != null && CompareTag(_lastKilledEnemyTag))
        {
            // 同じtagの敵を連続で倒した場合は、連続Killのカウントをアップ
            _chainKillCount++;
            Debug.Log($"{_lastKilledEnemyTag}: {_chainKillCount}");
        }
        else
        {
            _lastKilledEnemyTag = tag;
            _chainKillCount = 1;
        }


        if (needChainKill)
        {
            // 連続Killの条件チェック
            if (_chainKillCount < needChainKillCount)
            {
                return;
            }

            _chainKillCount = 0;
        }

        // 指定個数分のアイテムを出現させる
        for (var i = 0; i < number; i++)
        {
            var item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            item.Initialize();
        }

        // アイテムドロップ確率の計算
        if (Random.Range(0, 1f) >= dropRate) return;

        for (var i = 0; i < number; i++)
        {
            var item = Instantiate(itemPrefab2, transform.position, Quaternion.identity);
            item.Initialize();
        }
    }
}