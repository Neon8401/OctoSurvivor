using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;

public class PlayerController : MonoBehaviour
{


    //Vector3 CachedPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    //HashSet<ISpatialData3D> NearbyObstacles;


    private CharacterController _con;
    [SerializeField] private Animator _animator;
    [SerializeField] WeaponController _weapon;
    private PlayerStatus _status;
    private MobAttack _attack;


    private bool _isStart = false;
    private bool _ismove = false;

    float normalSpeed = 4f; // 通常時の移動速度
    float sprintSpeed = 8f; // ダッシュ時の移動速度
    float jump = 5f;        // ジャンプ力
    float gravity = 10f;    // 重力の大きさ

    Vector3 moveDirection = Vector3.zero;

    Vector3 startPos;

    void Start()
    {
        _con = GetComponent<CharacterController>();
        _status =GetComponent<PlayerStatus>();

        _attack = GetComponent<MobAttack>();

        // マウスカーソルを非表示にし、位置を固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        startPos = transform.position;
    }

    public void PlayerStart()
    {
        _isStart = !_isStart;
        _ismove = !_ismove;
    }


    void Update()
    {
        if (!_isStart) return;
        if (!_ismove) return;

        // 移動速度を取得
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        // カメラの向きを基準にした正面方向のベクトル
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算

        var moveZ = cameraForward * Input.GetAxis("Vertical") * speed;  //　前後（カメラ基準）　 
        var moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）


        //CachedPosition = transform.position;

        // isGrounded は地面にいるかどうかを判定します
        // 地面にいるときはジャンプを可能に
        if (_con.isGrounded)
        {
            _animator.SetBool("isGround", false);
            moveDirection = moveZ + moveX;

            
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jump;
                _animator.SetBool("isGround",true);
            }

            if ((moveZ + moveX).magnitude > 0 && Input.GetKey(KeyCode.Z))
            {
                _ismove = !_ismove;
                _animator.SetTrigger("Roll");
                moveDirection = (moveZ + moveX) * speed * 20;
                Wait().Forget();

            }
        }
        else
        {
            // 重力を効かせる
            moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);
            moveDirection.y -= gravity * Time.deltaTime;
            _animator.SetFloat("FallSpeed", moveDirection.y, 0.1f, Time.deltaTime);

            
        }
        // 移動のアニメーション
        _animator.SetFloat("Speed", (moveZ + moveX).magnitude, 0.1f, Time.deltaTime);

        // プレイヤーの向きを入力の向きに変更　
        transform.LookAt(transform.position + moveZ + moveX);

        // Move は指定したベクトルだけ移動させる命令
        _con.Move(moveDirection * Time.deltaTime);
        if (Input.GetButtonDown("Fire1"))
        {
            _ismove = !_ismove;
            if (_weapon.GunSet == false)
            {
                _animator.SetTrigger("Attack");
                AudioManager.instance.PlaySE("Swing");
            }
            _attack.AttackIfPossible();
            Debug.Log("Attack");
            Wait().Forget();

        }

        if (_status.Life <= 0) return;
        
    }

    public async UniTaskVoid Wait()
    {

        await UniTask.WaitForSeconds(0.6f);

        _ismove = !_ismove;
    }

}