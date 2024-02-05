using UnityEngine;

/// <summary>
/// 投げオノのマウスカーソル
/// </summary>
public class ThrowAxeCursor : MonoBehaviour
{
    private void Update()
    {
        // マウス座標を追従させる
        transform.position = Input.mousePosition;
    }
}