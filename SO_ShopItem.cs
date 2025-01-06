using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e����ScriptableObject
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/ShopItem")] 
public class SO_ShopItem : ScriptableObject
{
    [SerializeField] private string itemName, itemDesc; //�A�C�e���̖��O�A����
    [SerializeField] private Sprite itemSprite; //�A�C�e���̉摜
    [SerializeField] private int itemPrice; //�A�C�e���̉��i
    [SerializeField] private float itemEffectValue;
    [SerializeField] private string calcType;
    [SerializeField] private bool doSpecialMove;


    //======�Z�b�^�[�Q�b�^�[����======
    public string ItemName { get => itemName; set => itemName = value; }
    public string ItemDesc { get => itemDesc; set => itemDesc = value; }
    public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }
    public int ItemPrice { get => itemPrice; set => itemPrice = value; }
    public float ItemEffectValue { get => itemEffectValue; set => itemEffectValue = value; }
    public string CalcType { get => calcType; set => calcType = value; }
    public bool DoSpecialMove { get => doSpecialMove; set => doSpecialMove = value; }

    //================================

    /// <summary>
    /// SO_ShopItem�̃R���X�g���N�^
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
    /// �A�C�e�������擾���鏈��
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return this.itemName;
    }

    /// <summary>
    /// �A�C�e���̐������擾���鏈��
    /// </summary>
    /// <returns></returns>
    public string GetItemDesc()
    {
        return this.itemDesc;
    }

    /// <summary>
    /// �A�C�e���̉��i���擾���鏈��
    /// </summary>
    /// <returns></returns>
    public int GetPrice()
    {
        return this.itemPrice;
    }

/// <summary>
/// �A�C�e���̉摜���擾���鏈��
/// </summary>
/// <returns></returns>
    public Sprite GetItemSprite()
    {
            return this.itemSprite;
    }

/// <summary>
/// �A�C�e���̌��ʒl���擾���鏈��
/// </summary>
/// <returns></returns>
    public float GetEffectValue()
    {
        return this.itemEffectValue;
    }

/// <summary>
/// �A�C�e���̌v�Z�^�C�v���擾���鏈��
/// </summary>
/// <returns></returns>
    public string GetCalcType()
    {
        return this.calcType;
    }

/// <summary>
/// �A�C�e���̓�����ʂ��擾���鏈��
/// </summary>
/// <returns></returns>
    public bool IsSpecialMove()
    {
        return this.doSpecialMove;
    }
}
