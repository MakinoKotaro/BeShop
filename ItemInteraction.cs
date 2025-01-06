using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static UnityEditor.Progress;

/// <summary>
/// �J�[�\�����ǂ̃A�C�e����UI�ɏ�������𔻒肵�A������UI��\������X�N���v�g
/// </summary>
public class ItemInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private ShopUIManager shopUIManager; //ShopUiManager���擾
    [SerializeField] private PurchaseManager purchaseManager; //PurchaseManager���擾
    bool isShowing = false; //������UI���\�������ǂ���
    [SerializeField] SO_ShopItem shopItem; //SO_ShopItem���擾
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
            shopUIManager.ShowDescUI(shopItem);
            isShowing = true;
        }
    }

    /// <summary>
    /// �}�E�X�|�C���^���A�C�e����UI����o���Ƃ��̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        shopUIManager.HideDescUI();
        isShowing = false;
    }


    /// <summary>
    /// �A�C�e����UI���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        purchaseManager.SelectedItem(shopItem);
        itemPreview.ShowSelectedFrame(itemSelectedImage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gameObject"></param>
    public void HideFrame(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}

    