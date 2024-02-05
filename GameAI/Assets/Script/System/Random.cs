using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCount : MonoBehaviour
{
    public int Randomcount()
    {
        int rnd = Random.Range(1, 4);
        return rnd;
    }
}
