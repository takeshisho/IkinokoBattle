using UnityEngine;

public class ItemsDialog : MonoBehaviour
{
    [SerializeField] private int buttonNumber = 15;
    [SerializeField] private ItemButton itemButton;

    private ItemButton[] _itemButtons;

    private void Start() {
        // 初期状態は非表示
        gameObject.SetActive(false);

        // アイテム欄を必要な分だけ複製する
        for (var i = 0; i < buttonNumber - 1; i++)
        {
            // Instantiate(複製するオブジェクト, 複製先の親オブジェクト)
            Instantiate(itemButton, transform);
        }

        // 子要素のItemButtonを一括取得、保持しておく
        _itemButtons = GetComponentsInChildren<ItemButton>();
    }

    // アイテム欄を表示、非常時を切り替える
    public void Toggle()
    {
        // activeSelf: オブジェクトがアクティブかどうかを返す。
        // trueならアクティブなのでsetactive(false)となり非表示になる　falseならsetactive(true)となり表示される　要は表示非表示を切り替える
        gameObject.SetActive(!gameObject.activeSelf);

        // この段階では、表示されている場合がactiveSelfはtrueになるので、表示されている場合のみ処理を行う
        if(gameObject.activeSelf) {
            // 表示された場合はアイテム欄を更新する
            for (var i = 0; i < buttonNumber; i++)
            {
                // 各アイテムボタンに所持アイテムをセットする
                _itemButtons[i].OwnedItem = OwnedItemsData.Instance.OwnedItems.Length > i
                    ? OwnedItemsData.Instance.OwnedItems[i]
                    : null;
            }
        }
    }
}
