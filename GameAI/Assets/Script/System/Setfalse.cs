using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setfalse : MonoBehaviour
{

    public void OnRangeEnter(Collider collider)
    {
        this.gameObject.SetActive(false);
    }

    public void OnRangeOut(Collider collider)
    {
        this.gameObject.SetActive(true);
    }
}
