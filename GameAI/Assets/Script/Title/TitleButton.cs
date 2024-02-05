using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    void Start()
    {
        DOVirtual.DelayedCall(3f, () => SetUp());
    }

    void SetUp()
    {
        this.transform.DOMoveX(1920 - 500f, 1f);

        //現在の座標からY+5の座標へ3秒で移動する
        this.transform.DOMoveY(200f, 1f);

        //現在の座標からZ+5の座標へ3秒で移動する
        this.transform.DOMoveZ(0f, 1f);
    }

    public void OnClickTItle()
    {
        AudioManager.instance.PlaySE("Start");
        AudioManager.instance.StopBGM();
        SceneManager.LoadScene("TitleScene");
    }
}
