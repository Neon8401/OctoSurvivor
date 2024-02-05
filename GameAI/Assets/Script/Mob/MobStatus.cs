using UnityEditor;
using UnityEngine;
using static Charadata;
using static Lvdata;

/// <summary>
/// Mob（動くオブジェクト、MovingObjectの略）の状態管理スクリプト
/// </summary>
public abstract class MobStatus : MonoBehaviour
{
    /// <summary>
    /// 状態の定義
    /// </summary>
    protected enum StateEnum
    {
        Normal, // 通常
        Attack, // 攻撃中
        Die // 死亡
    }

    /// <summary>
    /// 移動可能かどうか
    /// </summary>
    public bool IsMovable => StateEnum.Normal == _state;

    /// <summary>
    /// 攻撃可能かどうか
    /// </summary>
    public bool IsAttackable => StateEnum.Normal == _state;

    /// <summary>
    /// ライフ最大値を返します
    /// </summary>
    public float LifeMax => _charadata.MAXHP;

    /// <summary>
    /// ライフの値を返します
    /// </summary>
    
    public float Life => _life;

    [SerializeField] private Charadata _charadata;
    [SerializeField] private Lvdata _exp;
    protected Animator _animator;

    protected StateEnum _state = StateEnum.Normal; // Mob状態
    private float _life; // 現在のライフ値（ヒットポイント）

    protected virtual void Start()
    {
        _life = _charadata.MAXHP; // 初期状態はライフ満タン
        _charadata.HP = (int) _life;


        
        _animator = GetComponentInChildren<Animator>();

        // ライフゲージの表示開始
        LifeGaugeContainer.Instance.AddLife(this);
    }

    private void Update()
    {
        _life =_charadata.HP;

        //OnTrue();
        // OnFalse();
    }

    /// <summary>
    /// キャラが倒れた時の処理を記述します。
    /// </summary>
    protected virtual void OnDie()
    {
        // ライフゲージの表示終了
        LifeGaugeContainer.Instance.RemoveLife(this);
    }

    /// <summary>
    /// 指定値のダメージを受けます。
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;
        Debug.Log(damage);
        _life -= damage;
        _charadata.HP = (int)_life;
        ///EditorUtility.SetDirty(_charadata);

        Debug.Log(_life);
        _animator.SetTrigger("Damage");
        AudioManager.instance.PlaySE("Damage");
        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");

        OnDie();
    }



    /// <summary>
    /// 可能であれば攻撃中の状態に遷移します。
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;
        if (_state == StateEnum.Die) return;
        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 可能であればNormalの状態に遷移します。
    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }

}