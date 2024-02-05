using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataMirror : MonoBehaviour
{

    public int MAXHP; //�ő�HP
    public int HP;//���݂�HP
    public int ATK; //�U����
    public int EXP; //�o���l
    public int LV; //���x��
    public int GETEXP; //�擾�o���l

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
