using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTextAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 終点として利用するので、初期座標を取得
        var transformCache = transform;
        var defaultPosition = transformCache.localPosition;
        // いったん上の方へ移動
        transformCache.localPosition = new Vector3(0, 300f);

        // アニメーションを開始する
        // Onconpleteにより、アニメーションの実行を待ってから次のアニメーションを仕込んでいる
        // 上から現れ画面中央に移動するアニメーション
        transformCache.DOLocalMove(defaultPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("GAMEOVER");
            // ジェイクアニメーション(移動完了時に振動するアニメーション)
            transformCache.DOShakePosition(1.5f, 100);
        });

        // 5秒待ってからTitleSceneに移動する。DoTweenにはcoroutineを使わなくても任意の秒数待てるメソッドもある。
        DOVirtual.DelayedCall(5, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }


}
