using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �h���b�v�_�E���̃R���g���[���[
/// </summary>
public class DropdownController : MonoBehaviour
{
    [SerializeField] SO_ShopItem knife; // �i�C�t�̃A�C�e��
    [SerializeField] TextMeshProUGUI itemName; // �A�C�e����
    [SerializeField] TextMeshProUGUI itemDesc; // �A�C�e������
    [SerializeField] Image itemImage; // �A�C�e���摜
    TMP_Dropdown dropdown; // �h���b�v�_�E��

    private bool isBought = false; // �w���ς݂��ǂ���

    public bool IsBought { get => isBought; set => isBought = value; }

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    /// <summary>
    /// �h���b�v�_�E�����I�����ꂽ�Ƃ��̏���
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
                    //�����ǉ�����̂��ȁH�H
                }
                break;
            default:
                itemName.text = "�A�C�e����";
                itemDesc.text = "�A�C�e������";
                itemImage.sprite = null; 
                break;
        }
    }

    /// <summary>
    /// �A�C�e�����w������
    /// </summary>
    public void BuyItem()
    {
        isBought = true;
    }

    //������āA�A�A�g���Ă���X�N���v�g�Ȃ̂��ȁH�H

}
