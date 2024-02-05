using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{
    [SerializeField] private Text messageText;
    private RectTransform _parentRectTransform;
    private Camera _camera;
    private Transform _target;

    private void Update()
    {
        Refresh();
    }

    public void Initialize(RectTransform parentRectTransform, Camera targetCamera, Transform target, string message,
        float duration)
    {
        transform.localScale = Vector3.zero;

        // 座標の計算に使うパラメータを受け取り、保持しておく

        _parentRectTransform = parentRectTransform;
        _camera = targetCamera;
        _target = target;
        messageText.text = message;
        Refresh();

        DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one, 0.1f))
            .AppendInterval(duration)
            .Append(transform.DOScale(Vector3.zero, 0.1f))
            .OnComplete(() => { Destroy(gameObject); });
    }

    private void Refresh()
    {
        if (_target == null) return;

        var screenPoint = _camera.WorldToScreenPoint(_target.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out var localPoint);
        transform.localPosition = localPoint + new Vector2(0, -80);
    }
}