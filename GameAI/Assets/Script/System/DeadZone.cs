using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            MobStatus _mobSt = other.gameObject.GetComponent<MobStatus>(); ;
            _mobSt.Damage(1000);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}