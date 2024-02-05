using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotBullet : MonoBehaviour
{
    public static ShotBullet instance;
    [SerializeField] Image _Aim;

    public CinemachineVirtualCameraBase vcam1;
    public CinemachineVirtualCameraBase vcam2;
    public GameObject bulletPrefab;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip unshotSound;
    public float shotSpeed;
    public int shotCount = 5;
    private float shotInterval;
    public bool Shot => _shot;

    private bool _shot = false;

    private void Start()
    {
        _shot = true;

        
    }
    private void Update()
    {
        Setup();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (vcam1.Priority == 0)
            {
                vcam1.Priority = 1;
                vcam2.Priority = 0;
                _Aim.gameObject.SetActive(false);
            }
            else if(vcam1.Priority == 1)
            {
                vcam1.Priority = 0;
                vcam2.Priority = 1;
                _Aim.gameObject.SetActive(true);
            }
        }

    }

    private void Setup()
    {
        
        if (!_shot) return;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shotInterval += 1;

            if (shotInterval % 5 == 0 && shotCount > 0)
            {

                shotCount -= 1;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(90, 90, 180));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
                Destroy(bullet, 3.0f);
                AudioManager.instance.PlaySE("Shot");
                _shot = false;
                Wait().Forget();
            }
            if(shotCount == 0)
            {
                AudioManager.instance.PlaySE("Unshot");
                _shot = false;
                Wait().Forget();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _shot = false;
            shotCount = 5;
            AudioManager.instance.PlaySE("Reload");
            ReloadWait().Forget();
        }
    }

    public async UniTaskVoid Wait()
    {
        

        await UniTask.WaitForSeconds(0.5f);

        _shot = true;
    }

    public async UniTaskVoid ReloadWait()
    {


        await UniTask.WaitForSeconds(1);

        _shot = true;
    }
}