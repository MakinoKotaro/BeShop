using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ドロップダウンのコントローラー
/// </summary>
public class DropdownController : MonoBehaviour
{
    [SerializeField] SO_ShopItem knife; // ナイフのアイテム
    [SerializeField] TextMeshProUGUI itemName; // アイテム名
    [SerializeField] TextMeshProUGUI itemDesc; // アイテム説明
    [SerializeField] Image itemImage; // アイテム画像
    TMP_Dropdown dropdown; // ドロップダウン

    private bool isBought = false; // 購入済みかどうか

    public bool IsBought { get => isBought; set => isBought = value; }

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    /// <summary>
    /// ドロップダウンが選択されたときの処理
    /// </summary>
    public void OnSelected()
    {
        switch(dropdown.value)
        {
            case 0:
                itemName.text = knife.GetItemName();
                itemDesc.text = knife.GetItemDesc();
                itemImage.sprite = knife.GetItemSprite();
                
                if(isBought == true)
                {
                    //何か追加するのかな？？
                }
                break;
            default:
                itemName.text = "アイテム名";
                itemDesc.text = "アイテム説明";
                itemImage.sprite = null; 
                break;
        }
    }

    /// <summary>
    /// アイテムを購入する
    /// </summary>
    public void BuyItem()
    {
        isBought = true;
    }

    //これって、、、使っているスクリプトなのかな？？

}
