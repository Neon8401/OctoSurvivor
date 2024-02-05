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

        //���݂̍��W����Y+5�̍��W��3�b�ňړ�����
        this.transform.DOMoveY(200f, 1f);

        //���݂̍��W����Z+5�̍��W��3�b�ňړ�����
        this.transform.DOMoveZ(0f, 1f);
    }

    public void OnClickTItle()
    {
        AudioManager.instance.PlaySE("Start");
        AudioManager.instance.StopBGM();
        SceneManager.LoadScene("TitleScene");
    }
}
