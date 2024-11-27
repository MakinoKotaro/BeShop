using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムのScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/ShopItem")] 
public class SO_ShopItem : ScriptableObject
{
    [SerializeField] private string itemName, itemDesc; //アイテムの名前、説明
    [SerializeField] private Sprite itemSprite; //アイテムの画像
    [SerializeField] private int itemPrice; //アイテムの価格
    [SerializeField] private float itemEffectValue;
    [SerializeField] private string calcType;
    [SerializeField] private bool doSpecialMove;


    //======セッターゲッターたち======
    public string ItemName { get => itemName; set => itemName = value; }
    public string ItemDesc { get => itemDesc; set => itemDesc = value; }
    public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }
    public int ItemPrice { get => itemPrice; set => itemPrice = value; }
    public float ItemEffectValue { get => itemEffectValue; set => itemEffectValue = value; }
    public string CalcType { get => calcType; set => calcType = value; }
    public bool DoSpecialMove { get => doSpecialMove; set => doSpecialMove = value; }

    //================================

    /// <summary>
    /// SO_ShopItemのコンストラクタ
    /// </summary>
    /// <param name="name"></param>
    /// <param name="desc"></param>
    /// <param name="price"></param>
    public SO_ShopItem(string name, string desc, int price, float effectValue, string calcType, bool doSpecialMove)
    {
        this.itemName = name;
        this.itemDesc = desc;
        this.itemPrice = price;
        this.itemEffectValue = effectValue;
        this.calcType = calcType;
        DoSpecialMove = doSpecialMove;
    }

    /// <summary>
    /// アイテム名を取得する処理
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return this.itemName;
    }

    /// <summary>
    /// アイテムの説明を取得する処理
    /// </summary>
    /// <returns></returns>
    public string GetItemDesc()
    {
        return this.itemDesc;
    }

    /// <summary>
    /// アイテムの価格を取得する処理
    /// </summary>
    /// <returns></returns>
    public int GetPrice()
    {
        return this.itemPrice;
    }

    //アイテムの画像を取得する処理
    public Sprite GetItemSprite()
    {
            return this.itemSprite;
    }

    public float GetEffectValue()
    {
        return this.itemEffectValue;
    }

    public string GetCalcType()
    {
        return this.calcType;
    }

    public bool IsSpecialMove()
    {
        return this.doSpecialMove;
    }
}
