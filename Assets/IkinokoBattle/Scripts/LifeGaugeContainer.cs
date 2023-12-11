using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private static LifeGaugeContainer _instance;

    [SerializeField] private Camera mainCamera; // ライフゲージの表示対象を写しているカメラ
    [SerializeField] private LifeGauge lifeGaugePrefab;

    private RectTransform rectTransform;
    // アクティブなライフゲージを保持するコンテナ
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap = new 
        Dictionary<MobStatus, LifeGauge>();

    private void Awake()
    {
        // シーンに一つしか存在させないスクリプトのためこのような擬似シングルトンが成り立つ
        if(null != _instance) throw new System.Exception("LifeBarContainer instance already exists");
        _instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    // ライフゲージを追加する
    public void Add(MobStatus status)
    {
        // ライフゲージを生成する
        var lifeGauge = Instantiate(lifeGaugePrefab, transform);
        lifeGauge.Initialize(rectTransform, mainCamera, status);
        _statusLifeBarMap.Add(status, lifeGauge);
    }

    // ライフゲージを削除する
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        // このRemoveは、DictionaryのRemoveメソッド
        _statusLifeBarMap.Remove(status);
    }
}
