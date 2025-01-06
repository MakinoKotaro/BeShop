using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �V���b�v��ʂɕ\������Ă���A�C�e���ɃJ�[�\����������Ƃ��ɕ\�����������UI���Ǘ�����X�N���v�g
/// </summary>
public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private Image panelImage; //�A�C�e�������̃p�l�����������

    [SerializeField] private TextMeshProUGUI itemNameText, itemDescText; //�p�l�����̃A�C�e�����Ɛ�������������

    [SerializeField] private Button buyButton; //�w���{�^�����������

    private PurchaseManager purchaseManager; //PurchaseManager���������

    [SerializeField] private TextMeshProUGUI playerInfoText_attackPower; //�v���C���[�̍U���͂�\������e�L�X�g
    [SerializeField] private TextMeshProUGUI playerInfoText_hitPoint; //�v���C���[��HP��\������e�L�X�g
    [SerializeField] private TextMeshProUGUI playerInfoText_speed; //�v���C���[�̃X�s�[�h��\������e�L�X�g
    [SerializeField] private GameObject playerParameterObj; //�v���C���[�̃p�����[�^�������Ă���I�u�W�F�N�g
    PlayerParameter playerParameter; //�v���C���[�̃p�����[�^�������Ă���X�N���v�g

    private void Start()
    {
        panelImage.GetComponent<Image>(); //panelImage����Image�R���|�[�l���g���擾
        

        itemNameText = itemNameText.GetComponent<TextMeshProUGUI>(); // itemNameText����TextMeshPro�R���|�[�l���g���擾
        
        itemDescText = itemDescText.GetComponent<TextMeshProUGUI>(); // itemDescText����TextMeshPro�R���|�[�l���g���擾
        
        if (playerInfoText_attackPower != null)
        {
            playerInfoText_attackPower = playerInfoText_attackPower.GetComponent<TextMeshProUGUI>();
        }
        purchaseManager = FindObjectOfType<PurchaseManager>(); //PurchaseManager�������Ă���I�u�W�F�N�g��T��

        playerParameter = playerParameterObj.GetComponent<PlayerParameter>();

        HideDescUI(); //�ŏ��́A����UI���B��
    }

    /// <summary>
    /// ����UI��\�����鏈��
    /// </summary>
    /// <param name="shopItem"></param>
    public void ShowDescUI(SO_ShopItem shopItem)
    {

        itemNameText.text = shopItem.ItemName;
        itemDescText.text = shopItem.ItemDesc;

        Color panelColor = panelImage.color;
        panelColor.a = 1;
        panelImage.color = panelColor;

        Color itemNameColor = itemNameText.color;
        itemNameColor.a = 1;
        itemNameText.color = itemNameColor;

        Color itemDescColor = itemDescText.color;
        itemDescColor.a = 1;
        itemDescText.color = itemDescColor;
    }

    /// <summary>
    /// ���@�̐���UI��\�����鏈��
    /// </summary>
    /// <param name="shopSpell"></param>
    public void ShowSpellDescUI(SO_Spell shopSpell)
    {
        itemNameText.text = shopSpell.SpellName;
        itemDescText.text = shopSpell.SpellDesc;

        Color panelColor = panelImage.color;
        panelColor.a = 1;
        panelImage.color = panelColor;

        Color itemNameColor = itemNameText.color;
        itemNameColor.a = 1;
        itemNameText.color = itemNameColor;

        Color itemDescColor = itemDescText.color;
        itemDescColor.a = 1;
        itemDescText.color = itemDescColor;
    }

    /// <summary>
    /// ����UI���\���ɂ��鏈��
    /// </summary>
    public void HideDescUI()
    {
        Color panelColor = panelImage.color;
        panelColor.a = 0;
        panelImage.color = panelColor;

        Color itemNameColor = itemNameText.color;
        itemNameColor.a = 0;
        itemNameText.color = itemNameColor;

        Color itemDescColor = itemDescText.color;
        itemDescColor.a = 0;
        itemDescText.color = itemDescColor;
    }

    private void Update()
    {
        if (playerInfoText_attackPower != null)
        {
            playerInfoText_attackPower.text = "Damage: \n" + playerParameter.PlayerPower.ToString();
            playerInfoText_hitPoint.text = "HP: \n" + playerParameter.PlayerHitPoint.ToString();
            playerInfoText_speed.text = "Speed: \n" + playerParameter.PlayerSpeed.ToString();
        }
    }
}


