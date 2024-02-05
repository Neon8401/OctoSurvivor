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
        // ‰ñ“]‚³‚¹‚È‚ª‚ç”CˆÓ‚Ì•ûŒü‚É”ò‚Î‚·
        _rigidbody.velocity = (to - from).normalized * speed;
        transform.DORotate(new Vector3(90, 90), 0.1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        // 1•b‚Å”jŠü
        _destroyTween = DOVirtual.DelayedCall(1f, () => { Destroy(gameObject); });
    }

    private void OnCollisionEnter(Collision other)
    {
        var targetMob = other.collider.GetComponent<MobStatus>();

        //AudioManager.instance.PlaySE("Break");
        if (null == targetMob) return;
        
        // “G‚É‚Ô‚Â‚©‚Á‚½‚çƒ_ƒ[ƒW‚ğ—^‚¦‚é
        targetMob.Damage(_charadata.ATK);
 

        // 1‰ñƒ_ƒ[ƒW‚ğ—^‚¦‚½‚ç“–‚½‚è”»’è‚ğ–³‚­‚·
        GetComponent<Collider>().enabled = false;
        transform.DOScale(Vector3.zero, 0.3f);

        // Initialize()‚Ì1•b‚Å”jŠü‚·‚éˆ—‚ğƒLƒƒƒ“ƒZƒ‹‚µAhitSound‚ª–Â‚èI‚í‚Á‚Ä‚©‚ç”jŠü
        _destroyTween.Kill();
        
    }
}
