using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        this.transform.DOMoveX(960f, 3f);

        //���݂̍��W����Y+5�̍��W��3�b�ňړ�����
        this.transform.DOMoveY(600f, 3f);

        //���݂̍��W����Z+5�̍��W��3�b�ňړ�����
        this.transform.DOMoveZ(0f, 3f);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            this.transform.DORotate(Vector3.up * 180f, 1f);
        }
    }
}
