using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour
{
    [SerializeField] Octree LinkedOctree;
    [SerializeField] float ObstacleSearchRange = 30f;

    Vector3 CachedPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    HashSet<ISpatialData3D> NearbyObstacles;

    bool HasMoved
    {
        get
        {
            return !Mathf.Approximately((transform.position - CachedPosition).sqrMagnitude, 0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (HasMoved)
        {
            CachedPosition = transform.position;

            HighlightNearbyObstacles();
        }
    }

    void HighlightNearbyObstacles()
    {
        HashSet<ISpatialData3D> CandidateObstacles = LinkedOctree.FindDataInRange(CachedPosition, ObstacleSearchRange);

        // 前にいた位置からObjectが指定位置から離れているか
        if (NearbyObstacles != null)
        {
            foreach (var OldObstacle in NearbyObstacles)
            {
                if (CandidateObstacles.Contains(OldObstacle))
                    continue;

                ProcessRemoveObstacle(OldObstacle);
            }
        }

        if (CandidateObstacles == null)
            return;

        // 初めて入った時
        if (NearbyObstacles == null)
        {
            NearbyObstacles = CandidateObstacles;
            foreach (var NewObstacle in NearbyObstacles)
                ProcessAddObstacle(NewObstacle);

            return;
        }

        // Objectが指定範囲内に入っているか
        foreach (var NewObstacle in CandidateObstacles)
        {
            if (NearbyObstacles.Contains(NewObstacle))
                continue;

            ProcessAddObstacle(NewObstacle);
        }

        NearbyObstacles = CandidateObstacles;
    }

    void ProcessAddObstacle(ISpatialData3D AddedObstacle)
    {
        (AddedObstacle as Obstacle3D).gameObject.SetActive(true);
    }

    void ProcessRemoveObstacle(ISpatialData3D RemovedObstacle)
    {
        // RemovedObstacleがnullでないかを確認
        if (RemovedObstacle != null)
        {
            // Obstacle3Dへのキャストを試みる前に、nullでないことを確認
            Obstacle3D obstacle = RemovedObstacle as Obstacle3D;
            if (obstacle != null)
            {
                // オブジェクトがアクティブでないことを確認してから非アクティブにする
                if (obstacle.gameObject.activeSelf)
                {
                    obstacle.gameObject.SetActive(false);
                }
            }
        }
    }
}