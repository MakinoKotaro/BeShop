using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カーソルがどのアイテムのUIに乗ったかを判定し、説明のUIを表示するスクリプト
/// </summary>
public class ShowDescription : MonoBehaviour
{
    [SerializeField] private ShopUiManager shopUIManager;
    [SerializeField] private PurchaseManager purchaseManager;

    [SerializeField] private GameObject[] itemImages = new GameObject[8];

    [SerializeField] SO_ShopItem[] shopItems = new SO_ShopItem[8];

//以下のコードの書き方はあまりよろしくない。どういうものにでも対応できる拡張性のあるアルゴリズムを考えよう
    //private void OnMouseEnter()
    //{
    //    Debug.Log("DWAD");
    //    Debug.Log("Mouse entered: " + gameObject.name);

    //    switch (gameObject.tag)
    //    {
    //        case "ShopItem1":
    //            shopUiManager.ShowDescUi(shopItems[0]);
    //            break;
    //        case "ShopItem2":
    //            shopUiManager.ShowDescUi(shopItems[1]);
    //            break;
    //        case "ShopItem3":
    //            shopUiManager.ShowDescUi(shopItems[2]);
    //            break;
    //        case "ShopItem4":
    //            shopUiManager.ShowDescUi(shopItems[3]);
    //            break;
    //        case "ShopItem5":
    //            shopUiManager.ShowDescUi(shopItems[4]);
    //            break;
    //        case "ShopItem6":
    //            shopUiManager.ShowDescUi(shopItems[5]);
    //            break;
    //        case "ShopItem7":
    //            shopUiManager.ShowDescUi(shopItems[6]);
    //            break;
    //        case "ShopItem8":
    //            shopUiManager.ShowDescUi(shopItems[7]);
    //            break;
    //        default:

    //            break;
    //    }
    //}

    //private void OnMouseExit()
    //{
    //    shopUiManager.HideDescUi();
    //}

}
