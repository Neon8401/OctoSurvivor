using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public bool Startcheck => _startcheck;
    private bool _startcheck = false;
    void Awake()
    {
        StartStrongButtonAnim();
    }
    void Start()
    {
        DOVirtual.DelayedCall(3f, () => SetUp());
    }

    void SetUp(){
        this.transform.DOMoveX(500f, 1f);

        //現在の座標からY+5の座標へ3秒で移動する
        this.transform.DOMoveY(200f, 1f);

        //現在の座標からZ+5の座標へ3秒で移動する
        this.transform.DOMoveZ(0f, 1f);
    }

    void StartStrongButtonAnim()
    {
        transform.DOScale(0.1f, 1f)
        .SetRelative(true)
        .SetEase(Ease.OutQuart)
        .SetLoops(-1, LoopType.Restart);
    }

    public void OnClickStart()
    {
        AudioManager.instance.PlaySE("Start");
        AudioManager.instance.StopBGM();
        _startcheck = !_startcheck;
        SceneManager.LoadScene("GameScene");
    }
}
