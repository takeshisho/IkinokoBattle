using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    // OwnedItemは所持アイテムを表すプロパティ
    public OwnedItemsData.OwnedItem OwnedItem 
    { 
        get {return _ownedItem;}
        set
        {
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

    private Button _button;
    private OwnedItemsData.OwnedItem _ownedItem;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        // ボタンを押した時の処理
    }

    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}
