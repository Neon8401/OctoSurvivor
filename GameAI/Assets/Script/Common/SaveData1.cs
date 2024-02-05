/// <summary>
/// セーブデータクラス
/// </summary>
public class SaveData1 : SavableSingletonBase<SaveData1>
{
    public int MAXHP = 2; //最大HP
    public int HP = 2;//現在のHP
    public int ATK = 1; //攻撃力
    public int EXP = 0; //経験値
    public int LV = 1; //レベル
    public int GETEXP = 10; //取得経験値
}