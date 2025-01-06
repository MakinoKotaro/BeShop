using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@��ScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/Spell")]
public class SO_Spell : ScriptableObject
{
    [SerializeField] private string spellName, spellDesc; //���@�̖��O�A����
    [SerializeField] private Sprite spellSprite; //���@�̉摜
    [SerializeField] private int spellPrice; //���@�̒l�i
    [SerializeField] private GameObject spellPrefab; //���@�̃v���n�u

    //======�v���p�e�B����======
    public string SpellName { get => spellName; set => spellName = value; }
    public string SpellDesc { get => spellDesc; set => spellDesc = value; }
    public Sprite SpellSprite { get => spellSprite; set => spellSprite = value; }
    public int SpellPrice { get => spellPrice; set => spellPrice = value; }
    public GameObject SpellPrefab { get => spellPrefab; set => spellPrefab = value; }
    //================================

    /// <summary>
    /// ���@��ScriptableObject�̃R���X�g���N�^
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
    /// ���@�����擾���鏈��
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return this.spellName;
    }

    /// <summary>
    /// ���@�̐������擾���鏈��
    /// </summary>
    /// <returns></returns>
    public string GetItemDesc()
    {
        return this.spellDesc;
    }

    /// <summary>
    /// ���@�̉��i���擾���鏈��
    /// </summary>
    /// <returns></returns>
    public int GetPrice()
    {
        return this.spellPrice;
    }

    /// <summary>
    /// ���@�̉摜���擾���鏈��
    /// </summary>
    /// <returns></returns>
    public Sprite GetItemSprite()
    {
        return this.spellSprite;
    }

    /// <summary>
    /// ���@�̃v���n�u���擾���鏈��
    /// </summary>
    /// <returns></returns>
    public GameObject GetPrefab()
    {
        return this.spellPrefab;
    }
    
}
