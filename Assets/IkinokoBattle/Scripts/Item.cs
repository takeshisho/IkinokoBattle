using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Wood, 
        Stone,
        ThrowAxe
    }

    [SerializeField] private ItemType type;

    public void Initialize()
    {
        // アニメーションが終わるまでcolliderを無効化
        var colliderCache = GetComponent<Collider>();
        colliderCache.enabled = false;

        // アニメーションの処理
        var transformCache = transform;
        var dropPosition = transform.localPosition + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        transformCache.DOLocalMove(dropPosition, 0.5f);

        var defaultScale = transformCache.localScale;
        transformCache.localScale = Vector3.zero;
        // SetEaseはアニメーションの種類を指定してる。https://game-ui.net/?p=835#toc43
        transformCache.DOScale(defaultScale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            // アニメーションが終了したら、colliderを有効化
            colliderCache.enabled = true;
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        OwnedItemsData.Instance.Add(type);
        OwnedItemsData.Instance.Save();
        foreach (var item in OwnedItemsData.Instance.OwnedItems)
        {
            Debug.Log(item.Type + "を" + item.Number + "個所持");
        }
        Destroy(gameObject);
    }
}
