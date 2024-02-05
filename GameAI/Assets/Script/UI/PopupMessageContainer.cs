using UnityEngine;

/// <summary>
/// ポップアップメッセージの制御クラス
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class PopupMessageContainer : SingletonMonoBehaviourInSceneBase<PopupMessageContainer>
{
    [SerializeField] private Camera mainCamera; // メッセージ表示対象のMobを映しているカメラ
    [SerializeField] private PopupMessage popupMessagePrefab;

    private RectTransform _rectTransform;

    protected override void Awake()
    {
        base.Awake();
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 任意のTransformの位置にメッセージを表示します。
    /// </summary>
    /// <param name="target"></param>
    /// <param name="message"></param>
    /// <param name="duration"></param>
    public void PopupMessage(Transform target, string message, float duration = 2f)
    {
        var popupMessage = Instantiate(popupMessagePrefab, transform);
        popupMessage.Initialize(_rectTransform, mainCamera, target, message, duration);
    }
}