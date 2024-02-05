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
        // ��]�����Ȃ���C�ӂ̕����ɔ�΂�
        _rigidbody.velocity = (to - from).normalized * speed;
        transform.DORotate(new Vector3(90, 90), 0.1f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
        // 1�b�Ŕj��
        _destroyTween = DOVirtual.DelayedCall(1f, () => { Destroy(gameObject); });
    }

    private void OnCollisionEnter(Collision other)
    {
        var targetMob = other.collider.GetComponent<MobStatus>();

        //AudioManager.instance.PlaySE("Break");
        if (null == targetMob) return;
        
        // �G�ɂԂ�������_���[�W��^����
        targetMob.Damage(_charadata.ATK);
 

        // 1��_���[�W��^�����瓖���蔻��𖳂���
        GetComponent<Collider>().enabled = false;
        transform.DOScale(Vector3.zero, 0.3f);

        // Initialize()��1�b�Ŕj�����鏈�����L�����Z�����AhitSound����I����Ă���j��
        _destroyTween.Kill();
        
    }
}
