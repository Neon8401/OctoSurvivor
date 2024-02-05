/// <summary>
/// セーブデータクラス
/// </summary>
public class SaveData : SavableSingletonBase<SaveData>
{

    public int HighScore;

    public int MAXHP = 10; //最大HP
    public int HP = 10;//現在のHP
    public int ATK = 2; //攻撃力
    public int EXP = 0; //経験値
    public int LV = 1; //レベル
    public int GETEXP = 0; //取得経験値
}