using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Warming : MonoBehaviour
{
    [SerializeField] Image DamageImg;

    [SerializeField] TimeManager _timeManager;
    void Start()
    {
        DamageImg.color = Color.clear;
    }


    void Update()
    {
        if (_timeManager.Warming) warming();
        DamageImg.color = Color.Lerp(DamageImg.color, Color.clear, Time.deltaTime);

    }

    private void warming()
    {
        DamageImg.color = new Color(0.7f, 0, 0, 0.7f);
        
    }
}