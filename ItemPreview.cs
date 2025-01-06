using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �V���b�v���ɃA�C�e���̏���\�����鏈��
/// </summary>
public class ItemPreview : MonoBehaviour
{
    [SerializeField]
    List<SO_ShopItem> items = new List<SO_ShopItem>(); //�V���b�v�̃A�C�e���̃��X�g

    //�A�C�e�����Ƃ̉摜�A���O�A���i�A�I�����ꂽ����p�̘g��ݒ肷��ϐ�
    [SerializeField] Image[] images = new Image[8];
    [SerializeField] TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[8];
    [SerializeField] TextMeshProUGUI[] priceTexts = new TextMeshProUGUI[8];
    [SerializeField] GameObject[] selectedFrames = new GameObject[8];

    // �z����g���̂͂܂��܂��������ǁA���X�g�̕����g�����������R�[�h�������邩�炨�����߁B
    // ���X�g�̓T�C�Y���_��ɕύX�ł��A�Ⴆ�ΐV�����A�C�e���𓮓I�ɒǉ�������폜�����肷��ꍇ�ɕ֗��ł��B
    // �܂��ALinq�Ȃǂ̃��\�b�h���g�����ƂŁA�f�[�^���삪�ȒP�ɂȂ�܂��B
    
    void Start()
    {
        //���ꂼ��̕ϐ��ɃA�C�e����ScriptableObject�̒��g����
        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = items[i].ItemSprite;
            nameTexts[i].text = items[i].ItemName;
            priceTexts[i].text = items[i].ItemPrice.ToString() + "yen";
        }
    }


    /// <summary>
    /// �I�����ꂽ���Ƃ��킩��₷������t���[����\��
    /// </summary>
    /// <param name="gameObject"></param>
    public void ShowSelectedFrame(GameObject gameObject)
    {
        foreach(GameObject g in selectedFrames)
        {
            g.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �I�����ꂽ�t���[�����\���ɂ���
    /// </summary>
    /// <param name="gameObject"></param>
    public void HideSelectedFrame(GameObject gameObject)
    {
        foreach (GameObject g in selectedFrames)
        {
            g.SetActive(false);
        }
    }

}
