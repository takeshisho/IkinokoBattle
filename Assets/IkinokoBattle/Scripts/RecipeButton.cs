using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RecipeButton : MonoBehaviour
{
    private Button _button;
    private OwnedItemsData ownedItemsData;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        
    }

    // 処理重くなるかもしれないけど、RecipeDialogがActiveのときは常にAxe作れるか判定してる。
    private void Update() {
        // InstanceはOwnedItemsDataのプロパティであり、getしかないので、代入しかできない。
        ownedItemsData = OwnedItemsData.Instance;
        _button.interactable = CanCreateThrowAxe();
    }

    private void OnClick() {  
        CreateThrowAxe();
    }

    private void CreateThrowAxe() {
        ownedItemsData.Use(Item.ItemType.Wood);
        ownedItemsData.Use(Item.ItemType.Stone);
        ownedItemsData.Add(Item.ItemType.ThrowAxe);
    }

    private bool CanCreateThrowAxe() {
        return ownedItemsData.OwnedItems.Any(x => x.Type == Item.ItemType.Wood && x.Number >= 1)
            && ownedItemsData.OwnedItems.Any(x => x.Type == Item.ItemType.Stone && x.Number >= 1);
    }
}
