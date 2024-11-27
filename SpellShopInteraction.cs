using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// カーソルがどの魔法のUIに乗ったかを判定し、説明のUIを表示するスクリプト
/// </summary>
public class SpellShopInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private ShopUiManager shopUiManager; //ShopUiManagerを取得
    [SerializeField] private PurchaseManager purchaseManager; //PurchaseManagerを取得
    bool isShowing = false; //説明のUIが表示中かどうか
    [SerializeField] SO_Spell shopSpell; //SO_ShopItemを取得
    [SerializeField] ItemPreview itemPreview; //ItemPreviewを取得
    [SerializeField] GameObject itemSelectedImage; //選択中なのをわかりやすくする画像を入れるもの

    void Start()
    {
        itemSelectedImage.SetActive(false); //選択中画像を非表示にする
    }

    /// <summary>
    /// マウスポインタがアイテムのUIに入ったときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isShowing == false)
        {
            shopUiManager.ShowSpellDescUi(shopSpell);
            isShowing = true;
        }
    }

    /// <summary>
    /// マウスポインタがアイテムのUIから出たときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        shopUiManager.HideDescUi();
        isShowing = false;
    }


    /// <summary>
    /// アイテムのUIがクリックされたときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        purchaseManager.SelectedSpell(shopSpell);
        itemPreview.ShowSelectedFrame(itemSelectedImage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    public void HideFrame(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
