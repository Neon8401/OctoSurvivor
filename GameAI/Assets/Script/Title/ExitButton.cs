using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

    void Start()
    {
        DOVirtual.DelayedCall(3f, () => SetUp());
    }

    void SetUp()
    {
        this.transform.DOMoveX(1920-500f, 1f);

        //���݂̍��W����Y+5�̍��W��3�b�ňړ�����
        this.transform.DOMoveY(200f, 1f);

        //���݂̍��W����Z+5�̍��W��3�b�ňړ�����
        this.transform.DOMoveZ(0f, 1f);
    }



    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}

