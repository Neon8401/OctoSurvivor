using static Charadata;
using UnityEngine;
using UnityEngine.UI;

public class LvUI : MonoBehaviour
{
    [SerializeField] Charadata _charadata;
    [SerializeField] Text _lv;

    // Update is called once per frame
    void Update()
    {
        int lv = _charadata.LV;

        string lvtext = ($"LV{lv}");
        _lv.text = lvtext;

    }
}