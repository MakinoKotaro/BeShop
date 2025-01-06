using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

/// <summary>
/// �A�C�e���w���̏���
/// </summary>
public class PurchaseManager : MonoBehaviour
{
    public List<SO_ShopItem> items; //�A�C�e����ScriptableObject�̃��X�g

    private SO_ShopItem selectedItem; //�I�����ꂽ�A�C�e������������
    private SO_Spell selectedSpell;
    [SerializeField] Image selectedItemImage; //�I�����ꂽ�A�C�e���̉摜��\������ꏊ����������
    [SerializeField] TextMeshProUGUI countText; //�I�����ꂽ�A�C�e�����w���������\������ꏊ����������
    private int itemCount = 0; //�A�C�e�����w�����鐔
    public int ItemCount { get => itemCount; set => itemCount = value; } //�A�C�e�����w�����鐔�̃Q�b�^�[�Z�b�^�[

    PlayerParameter playerParameter; //�v���C���[�̃p�����[�^�������Ă���X�N���v�g

    [SerializeField] private GameObject sFXManagerObj; //SFXManager���������

    private void Start()
    {
        countText = countText.GetComponent<TextMeshProUGUI>(); //countTest����TextMeshPro�R���|�[�l���g���擾
        selectedItemImage = selectedItemImage.GetComponent<Image>(); //selectedItemImage����Image�R���|�[�l���g���擾

        playerParameter = GetComponent<PlayerParameter>(); //PlayerParameter���擾
    }

    /// <summary>
    /// �ǂ̃A�C�e����I�����ꂽ���𔻒肷�鏈��
    /// </summary>
    /// <param name="item"></param>
    public void SelectedItem(SO_ShopItem item)
    {
        selectedItem = item;
        selectedItemImage.sprite = item.ItemSprite;
        Debug.Log($"�A�C�e���F{item.ItemName}���I������܂����B");
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetSelectBuyItemSound();

        CountReset();
    }

    /// <summary>
    /// �ǂ̖��@��I�����ꂽ���𔻒肷�鏈��
    /// </summary>
    /// <param name="spell"></param>
    public void SelectedSpell(SO_Spell spell)
    {
        selectedSpell = spell;
        selectedItemImage.sprite = spell.SpellSprite;
        Debug.Log($"���@�F{spell.SpellName}���I������܂����B");
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetSelectBuyItemSound();

        CountReset();
    }
    /// <summary>
    /// �A�C�e�����w�����鏈��
    /// </summary>
    public void BuyItems()
    {
        if (selectedItem != null && itemCount != 0)
        {
            Debug.Log($"�A�C�e���F{selectedItem.ItemName}��{itemCount}�w������܂����B");

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
    /// ���@���w�����鏈��
    /// </summary>
    public void BuySpells()
    {
        if (selectedSpell != null && itemCount != 0)
        {
            Debug.Log($"�A�C�e���F{selectedSpell.SpellName}��{itemCount}�w������܂����B");

            SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
            sFXManager.SetPurchaseSound();
        }

        CountReset();
    }
    
    /// <summary>
    /// �A�C�e���̍w�����𑝂₷�����i�{�^���ŌĂяo���j
    /// </summary>
    public void PlusItemCount()
    {
        itemCount++;
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetChangeItemAmountSound();
    }

    /// <summary>
    ///  �A�C�e���̍w���������炷�����i�{�^���ŌĂяo���j
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
    /// �A�C�e���̍w���������Z�b�g���鏈��
    /// </summary>
    public void CountReset()
    {
        itemCount = 0;
    }
    

    void Update()
    {
        //�I�����ꂽ�A�C�e������ʉE���ɕʂŕ\������ׂ̏���
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

        //�w������\������
        countText.text = itemCount.ToString();
    }
}
