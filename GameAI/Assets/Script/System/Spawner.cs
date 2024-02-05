using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem.Controls;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] float MinRadius = 0.5f;
    [SerializeField] float MaxRadius = 2f;
    [SerializeField] GameObject SpawnZone;
    [SerializeField] UnityEvent<Bounds> On3DBoundsCalculated = new();
    [SerializeField] UnityEvent<GameObject> OnItemSpawned = new();
    int index = 0;
    private void Start()
    {
        StartCoroutine(SpawnLoop()); // Coroutineを開始
    }
 
    /// <summary>
    /// 敵出現のCoroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnLoop()
    {
        var SpawnBounds = SpawnZone.GetComponent<MeshRenderer>().bounds;

        On3DBoundsCalculated.Invoke(SpawnBounds);

        List<GameObject> Items = new List<GameObject>(1000);

        while (true)
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

                //AudioManager.instance.PlaySE("Spawn");
                float Radius = Random.Range(MinRadius, MaxRadius);
                
                var NewGO = GameObject.Instantiate(enemyPrefab, navMeshHit.position, Quaternion.identity);
                NewGO.transform.localScale = new Vector3(Radius, Radius, Radius);
                NewGO.transform.SetParent(SpawnZone.transform);
                
                Items.Add(NewGO);
                //Octreeにデータを挿入
                OnItemSpawned.Invoke(NewGO);
                //Items[index].gameObject.SetActive(false);
            }

                // 1秒-α待つ
             yield return new WaitForSeconds(1f - Time.deltaTime);

            if (playerStatus.Life <= 0) 
            {
                // プレイヤーが倒れたらループを抜ける
                break;
            }

            index++;
        }
    }

}
