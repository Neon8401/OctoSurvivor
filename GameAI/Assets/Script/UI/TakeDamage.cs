using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour
{

    //�@DamageUI�v���n�u
    [SerializeField]
    private GameObject damageUI;

    public void Damage(Collider col)
    {
        //�@DamageUI���C���X�^���X���B�o��ʒu�͐ڐG�����R���C�_�̒��S����J�����̕����ɏ����񂹂��ʒu
        var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward * 0.2f, Quaternion.identity);
    }
}
