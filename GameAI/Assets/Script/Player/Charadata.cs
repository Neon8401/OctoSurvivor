using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Create StatusData",order =1)]
public class Charadata : ScriptableObject
{

        public int MAXHP; //最大HP
        public int HP;//現在のHP
        public int ATK; //攻撃力
        public int EXP; //経験値
        public int LV; //レベル
        public int GETEXP; //取得経験値
}

