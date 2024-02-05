using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject[] weapons;
    [SerializeField] GameObject _bulletCount;
    public int currentNum = 0;

    public bool GunSet => _gunSet;
    private bool _gunSet = false;
    void Start()
    {

        for (int i = 0; i < weapons.Length; i++)
        {

            if (i == currentNum)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            currentNum = (currentNum + 1) % weapons.Length;

            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == currentNum)
                {
                    weapons[i].SetActive(true);
                    _bulletCount.gameObject.SetActive(true);
                    _gunSet = true;
                }
                else
                {
                    weapons[i].SetActive(false);
                    _bulletCount.gameObject.SetActive(false);
                    _gunSet = false;
                }
                //if(i == 2){ _bulletCount.gameObject.SetActive(true); }
                
            }
        }
    }
}