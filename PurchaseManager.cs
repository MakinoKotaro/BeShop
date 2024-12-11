using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// アイテム購入の処理
/// </summary>
public class PurchaseManager : MonoBehaviour
{
    public List<SO_ShopItem> items; //アイテムのScriptableObjectのリスト

    private SO_ShopItem selectedItem; //選択されたアイテムをいれるもの
    private SO_Spell selectedSpell;
    [SerializeField] Image selectedItemImage; //選択されたアイテムの画像を表示する場所をいれるもの
    [SerializeField] TextMeshProUGUI countText; //選択されたアイテムを購入する個数を表示する場所をいれるもの
    private int itemCount = 0; //アイテムを購入する数
    public int ItemCount { get => itemCount; set => itemCount = value; } //アイテムを購入する数のゲッターセッター

    PlayerParameter playerParameter; //プレイヤーのパラメータを持っているスクリプト

    [SerializeField] private GameObject sFXManagerObj; //SFXManagerを入れるもの

    private void Start()
    {
        countText = countText.GetComponent<TextMeshProUGUI>(); //countTest内のTextMeshProコンポーネントを取得
        selectedItemImage = selectedItemImage.GetComponent<Image>(); //selectedItemImage内のImageコンポーネントを取得

        playerParameter = GetComponent<PlayerParameter>(); //PlayerParameterを取得
    }

    /// <summary>
    /// どのアイテムを選択されたかを判定する処理
    /// </summary>
    /// <param name="item"></param>
    public void SelectedItem(SO_ShopItem item)
    {
        selectedItem = item;
        selectedItemImage.sprite = item.ItemSprite;
        Debug.Log($"アイテム：{item.ItemName}が選択されました。");
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetSelectBuyItemSound();

        CountReset();
    }

    /// <summary>
    /// どの魔法を選択されたかを判定する処理
    /// </summary>
    /// <param name="spell"></param>
    public void SelectedSpell(SO_Spell spell)
    {
        selectedSpell = spell;
        selectedItemImage.sprite = spell.SpellSprite;
        Debug.Log($"魔法：{spell.SpellName}が選択されました。");
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetSelectBuyItemSound();

        CountReset();
    }
    /// <summary>
    /// アイテムを購入する処理
    /// </summary>
    public void BuyItems()
    {
        if (selectedItem != null && itemCount != 0)
        {
            Debug.Log($"アイテム：{selectedItem.ItemName}が{itemCount}個購入されました。");

            SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
            sFXManager.SetPurchaseSound();

            if (selectedItem.ItemDesc.Contains("+"))
            {
                playerParameter.AddParameter(selectedItem.CalcType, selectedItem.ItemEffectValue, itemCount);
            }
            else if (selectedItem.ItemDesc.Contains("*"))
            {
                playerParameter.MulParameter(selectedItem.CalcType, selectedItem.ItemEffectValue, ItemCount);
            }
            else
            {
                playerParameter.UnlockSpecialMove(selectedItem.name);
            }
            
            selectedItem = null;
        }

        CountReset();
    }

    /// <summary>
    /// 魔法を購入する処理
    /// </summary>
    public void BuySpells()
    {
        if (selectedSpell != null && itemCount != 0)
        {
            Debug.Log($"アイテム：{selectedSpell.SpellName}が{itemCount}個購入されました。");

            SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
            sFXManager.SetPurchaseSound();
        }

        CountReset();
    }
    
    /// <summary>
    /// アイテムの購入個数を増やす処理（ボタンで呼び出し）
    /// </summary>
    public void PlusItemCount()
    {
        itemCount++;
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetChangeItemAmountSound();
    }

    /// <summary>
    ///  アイテムの購入個数を減らす処理（ボタンで呼び出し）
    /// </summary>
    public void MinusItemCount()
    {
        if (itemCount > 0)
        {
            itemCount--;
            SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
            sFXManager.SetChangeItemAmountSound();
        }
    }

    /// <summary>
    /// アイテムの購入個数をリセットする処理
    /// </summary>
    public void CountReset()
    {
        itemCount = 0;
    }
    

    void Update()
    {
        //選択されたアイテムを画面右側に別で表示する為の処理
        if (selectedSpell == null && selectedItem == null)
        {
            itemCount = 0;
            Color color = selectedItemImage.color;
            color.a = 0;
            selectedItemImage.color = color;
        }
        else
        {
            Color color = selectedItemImage.color;
            color.a = 1;
            selectedItemImage.color = color;
        }

        //購入個数を表示する
        countText.text = itemCount.ToString();
    }
}
