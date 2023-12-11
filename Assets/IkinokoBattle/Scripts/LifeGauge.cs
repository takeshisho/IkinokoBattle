using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    // RectTransformはUIの位置やサイズを管理するコンポーネント
    private RectTransform _parentRectTransform;
    private Camera _camera;
    private MobStatus _status;

    private void Update() {
        Refresh();
    }

    // ゲージを初期化する
    public void Initialize(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
        Refresh();
    }

    // ゲージを更新する
    private void Refresh()
    {
        // 残りライフを表示する
        fillImage.fillAmount = _status.Life / _status.LifeMax;

        // 対象のMobの場所にゲージを移動する。world座標やlocal座標を変換するときは、RectTransformUtilityを使う
        var ScreenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;
        // 今回は、CanvasのRender ModeがScreen Space - Overlayなので、第３引数はnullでいい
        // Screen Space - Cameraの場合は、第３引数に対象のカメラを指定する必要がある。
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentRectTransform, ScreenPoint, null, out localPoint);

        // ゲームキャラに重ならないように少し上にずらす
        transform.localPosition = localPoint + new Vector2(0, 80);

    }
}
