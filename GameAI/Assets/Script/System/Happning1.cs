using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Happning1 : MonoBehaviour
{
    //public static Happning1 instance;

    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] TimeManager _timeManager;
    [SerializeField] float MinRadius = 0.5f;
    [SerializeField] float MaxRadius = 2f;
    [SerializeField] GameObject SpawnZone;
    [SerializeField] UnityEvent<Bounds> On3DBoundsCalculated = new();
    [SerializeField] UnityEvent<GameObject> OnItemSpawned = new();
    [SerializeField] RandomCount _randomcount;
    int count = 30;
    int index = 0;
    private bool _setTrue = false;
    // Start is called before the first frame update
    List<GameObject> Items2 = new List<GameObject>();
    void Start()
    {
        var SpawnBounds = SpawnZone.GetComponent<MeshRenderer>().bounds;



        On3DBoundsCalculated.Invoke(SpawnBounds);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_timeManager.Gimic) return;
        _setTrue = true;
        FallMeteo().Forget();
    }
    public async UniTask FallMeteo()
    {

        if (!_setTrue) return;
        while (count > 0)
        {
            // 距離10のベクトル
            var distanceVector = new Vector3(10, 100);
            // プレイヤーの位置をベースにした敵の出現位置。Y軸に対して上記ベクトルをランダムに0°〜360°回転させている
            var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;
            // 敵を出現させたい位置を決定
            var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

            // 指定座標から一番近いNavMeshの座標を探す
                
            float Radius = Random.Range(MinRadius, MaxRadius);
            //GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            // enemyPrefabを複製、NavMeshAgentは必ずNavMesh上に配置する
            var NewGO = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            NewGO.transform.localScale = new Vector3(Radius, Radius, Radius);
            //NewGO.transform.SetParent(SpawnZone.transform);
            Items2.Add(NewGO);
            OnItemSpawned.Invoke(NewGO);
            //Debug.Log(index);
            Items2[index].gameObject.SetActive(false);

            await UniTask.WaitForSeconds(2);

            count--;
        }
        if(count == 0)
        {
            _setTrue = false;
            count = 30;
        }
    }
}
