using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal.Profiling.Memory.Experimental;

[Serializable]
public class OwnedItemsData
{
    // const 定数
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA";

    // インスタンスを返す
    public static OwnedItemsData Instance
    {
        get
        {
            if(null == _instance)
            {
                // PlayerPrefs.HasKey: 指定したキーが存在するかどうかを返す
                // JsonUtility.FromJson<型名>(Json文字列): Json文字列をオブジェクトに変換する
                _instance = PlayerPrefs.HasKey(PlayerPrefsKey)
                    ? JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.
                        GetString(PlayerPrefsKey)) : new OwnedItemsData();
            }

            return _instance;
        }
    }

    private static OwnedItemsData _instance;

    // 所持アイテム一覧取得
    public OwnedItem[] OwnedItems
    {
        // 大文字と小文字間違えないように！！ミスるとクラッシュしやがる。
        // ToArray(): Listを配列に変換する
        get { return ownedItems.ToArray(); }
    }

    // どのアイテムを何個所持しているかのリスト
    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>();

    // コンストラクタ
    // シングルトンでは、外部からnewできないようにコンストラクタをprivateにする
    private OwnedItemsData()
    {
    }

    // Json化してPlayerPrefsに保存する。
    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        // PlayerPrefs.SetString(key, string): 指定したキーに文字列を保存する
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    // itemを追加する
    public void Add(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if(null == item)
        {
            item = new OwnedItem(type);
            ownedItems.Add(item);
        }
        item.Add(number);
    }

    public void Use(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if(null == item || item.Number < number)
        {
            // throw new Exception: 例外を発生させる
            throw new Exception("アイテムが足りません");
        }
        item.Use(number);
    }

    // 対象の種類のアイテムデータを取得
    public OwnedItem GetItem(Item.ItemType type)
    {
        // FirstOrDefault: 条件に一致する最初の要素を返す。見つからない場合はnullを返す
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }

    // アイテムの所持数管理モデル
    [Serializable]
    public class OwnedItem
    {
        public Item.ItemType Type
        {
            get { return type; }
        }
        public int Number
        {
            get{ return number; }
        }


        [SerializeField] private Item.ItemType type; // アイテムの種類
        [SerializeField] private int number; // 所持個数
        
        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }
        public void Add(int number = 1)
        {
            this.number += number;
        }
        public void Use(int number = 1)
        {
            this.number -= number;
        }
    }
}



