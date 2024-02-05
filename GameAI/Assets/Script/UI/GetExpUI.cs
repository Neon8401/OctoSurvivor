using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static Charadata;
using static Lvdata;

public class GetExpUI : MonoBehaviour
{
    [SerializeField] Image _ExpImage;
    [SerializeField] Charadata _charadata;
    [SerializeField] private Lvdata _exp;

    void GetExp()
    {
        var exp = _exp.playerExpTable[_charadata.LV];
        _ExpImage.fillAmount = _charadata.EXP / exp.exp;
        if (_charadata.EXP >= exp.exp)
        {
            _ExpImage.fillAmount = 0;
        }
    }

}