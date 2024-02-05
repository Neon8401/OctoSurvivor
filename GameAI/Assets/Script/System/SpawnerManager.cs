using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private const float DistanceLimitFromPlayer = 20f;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private int max;

    public bool IsSpawnable => _enemies.Count < max;

    private List<GameObject> _enemies = new List<GameObject>();

    public void RefreshEnemies()
    {
        // �v���C���[����̋��������𒴂���ꍇ�A�G�L������Destroy����
        var removeTargetEnemies = _enemies.Where(
            x => null != x
                 && (x.transform.localPosition - _playerController.transform.localPosition).magnitude > DistanceLimitFromPlayer);
        foreach (var enemy in removeTargetEnemies)
        {
            Destroy(enemy.gameObject);
        }

        // Destroy���ꂽObject��null�ɂȂ�̂ŁA����
        _enemies = _enemies.Where(x => x != null).ToList();
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }
}