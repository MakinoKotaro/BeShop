using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �J�[�\�����ǂ̖��@��UI�ɏ�������𔻒肵�A������UI��\������X�N���v�g
/// </summary>
public class SpellShopInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private ShopUiManager shopUIManager; //ShopUiManager���擾
    [SerializeField] private PurchaseManager purchaseManager; //PurchaseManager���擾
    bool isShowing = false; //������UI���\�������ǂ���
    [SerializeField] SO_Spell shopSpell; //SO_ShopItem���擾
    [SerializeField] ItemPreview itemPreview; //ItemPreview���擾
    [SerializeField] GameObject itemSelectedImage; //�I�𒆂Ȃ̂��킩��₷������摜���������

    void Start()
    {
        itemSelectedImage.SetActive(false); //�I�𒆉摜���\���ɂ���
    }

    /// <summary>
    /// �}�E�X�|�C���^���A�C�e����UI�ɓ������Ƃ��̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isShowing == false)
        {
            shopUIManager.ShowSpellDescUi(shopSpell);
            isShowing = true;
        }
    }

    /// <summary>
    /// �}�E�X�|�C���^���A�C�e����UI����o���Ƃ��̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        shopUIManager.HideDescUi();
        isShowing = false;
    }


    /// <summary>
    /// �A�C�e����UI���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        purchaseManager.SelectedSpell(shopSpell);
        itemPreview.ShowSelectedFrame(itemSelectedImage);
    }

    /// <summary>
    /// �I�𒆂̉摜���\���ɂ���
    /// </summary>
    /// <param name="gameObject"></param>
    public void HideFrame(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
