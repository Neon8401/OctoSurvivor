using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using static Charadata;


[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    [SerializeField] Charadata _charadata;
    private Rigidbody _rigidbody;
    private Tween _destroyTween;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 from, Vector3 to, float speed)
    {
        transform.position = from;
        // 回転させながら任意の方向に飛ばす
        _rigidbody.velocity = (to - from).normalized * speed;
        transform.DORotate(new Vector3(90, 90), 0.1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        // 1秒で破棄
        _destroyTween = DOVirtual.DelayedCall(1f, () => { Destroy(gameObject); });
    }

    private void OnCollisionEnter(Collision other)
    {
        var targetMob = other.collider.GetComponent<MobStatus>();

        //AudioManager.instance.PlaySE("Break");
        if (null == targetMob) return;
        
        // 敵にぶつかったらダメージを与える
        targetMob.Damage(_charadata.ATK);
 

        // 1回ダメージを与えたら当たり判定を無くす
        GetComponent<Collider>().enabled = false;
        transform.DOScale(Vector3.zero, 0.3f);

        // Initialize()の1秒で破棄する処理をキャンセルし、hitSoundが鳴り終わってから破棄
        _destroyTween.Kill();
        
    }
}
