using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using static Charadata;


[RequireComponent(typeof(Rigidbody))]
public class MeteoDamage : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Tween _destroyTween;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 from, Vector3 to, float speed)
    {
        transform.position = from;
        // âÒì]Ç≥ÇπÇ»Ç™ÇÁîCà”ÇÃï˚å¸Ç…îÚÇŒÇ∑
        _rigidbody.velocity = (to - from).normalized * speed;
        transform.DORotate(new Vector3(90, 90), 0.1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        // 1ïbÇ≈îjä¸
        _destroyTween = DOVirtual.DelayedCall(1f, () => { Destroy(gameObject); });
    }

    private void OnCollisionEnter(Collision other)
    {
        var targetMob = other.collider.GetComponent<MobStatus>();
        if (null == targetMob) return;
        if (targetMob.CompareTag("Player"))
        {
            AudioManager.instance.PlaySE("Break");
            // ìGÇ…Ç‘Ç¬Ç©Ç¡ÇΩÇÁÉ_ÉÅÅ[ÉWÇó^Ç¶ÇÈ
            targetMob.Damage(10);
        }

            Destroy(gameObject);
    }
}
