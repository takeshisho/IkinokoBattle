using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    // OwnedItemは所持アイテムを表すプロパティ
    // OwnedItemsData.OwnedItemは返り値の型　OwnedItemはプロパティ名
    public OwnedItemsData.OwnedItem OwnedItem 
    { 
        get {return _ownedItem;}
        set
        {
            // valueはsetされた値を表す
            _ownedItem = value;

            // アイテムが割り当てられたかどうかでアイテム画像や所持個数の表示を切り替える

            // _ownedItemがある時つまり、null出ない時false, nullの時true
            var isEmpty = null == _ownedItem;
            image.gameObject.SetActive(!isEmpty);
            number.gameObject.SetActive(!isEmpty);

            // interatableはボタンを押せるかどうかを切り替える
            _button.interactable = !isEmpty;

            if(!isEmpty) {
                // アイテム画像、所持個数をセット
                // image.spriteはImageコンポーネントの画像を表すプロパティ
                // itemSpritesの中から、_ownedItem.Typeと同じitemTypeを持つ要素を取得し、そのspriteを取得する
                image.sprite = itemSprites.First(x => x.itemType == _ownedItem.Type).sprite;
                number.text = "" + _ownedItem.Number;
            }
        } 
    }

    [SerializeField] private ItemTypeSpriteMap[] itemSprites;
    [SerializeField] private Image image;
    [SerializeField] private TMPro.TextMeshProUGUI number;
    [SerializeField] private GameObject ThrowAxePrefab;
    // キャラの左手を指定する予定
    [SerializeField] private GameObject ThrowAxeSpawnPoint;
    public UnityAction CreateAction;

    private Button _button;
    private OwnedItemsData.OwnedItem _ownedItem;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        // _ownedItemは、OwnedItemsData.csにあるクラス
        // OwnedItemsDataクラスの子クラスである、OwnedItemクラスのプロパティTypeを呼び出している
        // TODO: アイテムの出現場所を正しくする。
        if(_ownedItem.Type == Item.ItemType.ThrowAxe) {
            // 妥協して調整した出すようにした、、、。
            Instantiate(ThrowAxePrefab, ThrowAxeSpawnPoint.transform.position + new Vector3(0.45f, 0.7f, 0), Quaternion.Euler(0, 0, 180));
            _ownedItem.Use();
            // action使用して、アイテム欄を更新する
            CreateAction?.Invoke();
        }
    }

    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}
