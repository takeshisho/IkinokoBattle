using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameOverTextAnimator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        StartCoroutine(GameOverAnimation());
    }

    private IEnumerator GameOverAnimation()
    {
        // 終点として利用するので、初期座標を取得
        var transformCache = transform;
        var defaultPosition = transformCache.localPosition;
        var lastPosition = transform.localPosition + new Vector3(0, 140f);
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

        yield return new WaitForSeconds(3f);

        transformCache.DOLocalMove(lastPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Score表示
            scoreText.text = "score: " + Score.Instance.GameScore.ToString();
        });

        // 5秒待ってからTitleSceneに移動する。DoTweenにはcoroutineを使わなくても任意の秒数待てるメソッドもある。
        DOVirtual.DelayedCall(5, () =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }


}
