using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法のScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/Spell")]
public class SO_Spell : ScriptableObject
{
    [SerializeField] private string spellName, spellDesc; //魔法の名前、説明
    [SerializeField] private Sprite spellSprite; //魔法の画像
    [SerializeField] private int spellPrice; //魔法の値段
    [SerializeField] private GameObject spellPrefab; //魔法のプレハブ

    //======プロパティたち======
    public string SpellName { get => spellName; set => spellName = value; }
    public string SpellDesc { get => spellDesc; set => spellDesc = value; }
    public Sprite SpellSprite { get => spellSprite; set => spellSprite = value; }
    public int SpellPrice { get => spellPrice; set => spellPrice = value; }
    public GameObject SpellPrefab { get => spellPrefab; set => spellPrefab = value; }
    //================================

    /// <summary>
    /// 魔法のScriptableObjectのコンストラクタ
    /// </summary>
    /// <param name="name"></param>
    /// <param name="desc"></param>
    /// <param name="sprite"></param>
    /// <param name="price"></param>
    /// <param name="prefab"></param>
    public SO_Spell(string name, string desc, Sprite sprite, int price, GameObject prefab)
    {
        this.spellName = name;
        this.spellDesc = desc;
        this.spellSprite = sprite;
        this.spellPrice = price;
        this.spellPrefab = prefab;
    }

    /// <summary>
    /// 魔法名を取得する処理
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return this.spellName;
    }

    /// <summary>
    /// 魔法の説明を取得する処理
    /// </summary>
    /// <returns></returns>
    public string GetItemDesc()
    {
        return this.spellDesc;
    }

    /// <summary>
    /// 魔法の価格を取得する処理
    /// </summary>
    /// <returns></returns>
    public int GetPrice()
    {
        return this.spellPrice;
    }

    /// <summary>
    /// 魔法の画像を取得する処理
    /// </summary>
    /// <returns></returns>
    public Sprite GetItemSprite()
    {
        return this.spellSprite;
    }

    /// <summary>
    /// 魔法のプレハブを取得する処理
    /// </summary>
    /// <returns></returns>
    public GameObject GetPrefab()
    {
        return this.spellPrefab;
    }
    
}
