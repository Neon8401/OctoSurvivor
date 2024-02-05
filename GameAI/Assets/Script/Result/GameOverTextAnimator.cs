using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTextAnimator : MonoBehaviour
{
    StartButton _startButton;
    private void Start()
    {
        var transformCache = transform;
        // 終点として使用するため、初期座標を保持
        var defaultPosition = transformCache.localPosition;
        // いったん上の方に移動させる
        
        transformCache.localPosition = new Vector3(0, 300f);
        // 移動アニメーション開始
        transformCache.DOLocalMove(defaultPosition, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("GAME OVER!!");
                // シェイクアニメーション
                transformCache.DOShakePosition(1.5f, 100);
            });
        if (_startButton.Startcheck == true) return;
        // DOTweenには、Coroutineを使わずに任意の秒数を待てる便利メソッドも搭載されている
        DOVirtual.DelayedCall(15, () =>
        {
            // 15秒待ってからタイトルシーンに遷移
            SceneManager.LoadScene("TitleScene");
        });
    }
}