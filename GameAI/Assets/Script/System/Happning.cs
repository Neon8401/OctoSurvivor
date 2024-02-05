using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class Happning : MonoBehaviour
{

    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject meteoPrefab;
    [SerializeField] TimeManager _timeManager;
    [SerializeField] float MinRadius = 0.5f;
    [SerializeField] float MaxRadius = 2f;
    [SerializeField] GameObject SpawnZone;
    [SerializeField] GameObject SpawnZone2;
    [SerializeField] UnityEvent<Bounds> On3DBoundsCalculated = new();
    [SerializeField] UnityEvent<GameObject> OnItemSpawned = new();
    [SerializeField] RandomCount _randomcount;
    private int spcount = 30;
    private int mtcount = 30;
    private int _con = 1;
    private bool _setTrue = false;


    // Update is called once per frame
    private void Start()
    {
        _setTrue = true;

        var SpawnBounds = SpawnZone.GetComponent<MeshRenderer>().bounds;
        var SpawnBounds2 = SpawnZone2.GetComponent<MeshRenderer>().bounds;


        On3DBoundsCalculated.Invoke(SpawnBounds);
        On3DBoundsCalculated.Invoke(SpawnBounds2);
    }
    List<GameObject> Items1 = new List<GameObject>();
    List<GameObject> Items2 = new List<GameObject>();
    private void Update()
    {
        if (!_timeManager.Gimic) return;
        _setTrue = true;
        SetUp().Forget();
    }
    private async UniTask SetUp() {
        if (!_setTrue) return;
        int rand = _randomcount.Randomcount();
        
        Debug.Log("rand" + rand);
        if (rand == 1)
        {


            while (spcount > 0)
            {
                // 距離10のベクトル
                var distanceVector = new Vector3(10, 0);
                // プレイヤーの位置をベースにした敵の出現位置。Y軸に対して上記ベクトルをランダムに0°〜360°回転させている
                var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;
                // 敵を出現させたい位置を決定
                var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

                // 指定座標から一番近いNavMeshの座標を探す
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
                {
                    float Radius = Random.Range(MinRadius, MaxRadius);

                    var NewGO = GameObject.Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
                    NewGO.transform.localScale = new Vector3(Radius, Radius, Radius);
                    NewGO.transform.SetParent(SpawnZone.transform);

                    Items1.Add(NewGO);
                    //Octreeにデータを挿入
                    OnItemSpawned.Invoke(NewGO);
                    //Items1[index].gameObject.SetActive(false);
                }

                //await UniTask.WaitForSeconds(1);


                if (playerStatus.Life <= 0)
                {
                    // プレイヤーが倒れたらループを抜ける
                    break;
                }
                spcount--;


            }
            if(spcount == 0)
            {
                _setTrue = false;
                spcount = _con * 20;
                _con += 1;
            }
        }
        if (rand == 2)
        {
            RenderSettings.fogDensity = 0.3f;

            await UniTask.Delay(TimeSpan.FromSeconds(10));

            RenderSettings.fogDensity = 0.01f;

            _setTrue = false;

        }
        if (rand == 3)
        {
           

            while (mtcount > 0)
            {
                // 距離10のベクトル
                var distanceVector = new Vector3(Random.Range(0,10),100);
                // プレイヤーの位置をベースにした敵の出現位置。Y軸に対して上記ベクトルをランダムに0°〜360°回転させている
                var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;
                // 敵を出現させたい位置を決定
                var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

                // 指定座標から一番近いNavMeshの座標を探す

                float Radius = Random.Range(MinRadius, MaxRadius);

                var NewGO = GameObject.Instantiate(meteoPrefab, spawnPosition, Quaternion.identity);
                NewGO.transform.localScale = new Vector3(Radius, Radius, Radius);



                mtcount--;
            }
            if (mtcount == 0)
            {
                _setTrue = false;
                mtcount = 30;
            }

        }
        else { _setTrue = false;    } 
        
    }

}

