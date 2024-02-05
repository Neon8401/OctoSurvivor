using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataMirror : MonoBehaviour
{

    public int MAXHP; //最大HP
    public int HP;//現在のHP
    public int ATK; //攻撃力
    public int EXP; //経験値
    public int LV; //レベル
    public int GETEXP; //取得経験値

    public  DataMirror()
    {

        this.MAXHP = 1;
        this.HP = 1;
        this.ATK = 1;
        this.EXP = 1;
        this.LV = 1;
        this.GETEXP = 1;
    }
}
