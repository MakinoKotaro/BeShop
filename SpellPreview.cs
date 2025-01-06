using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���@�V���b�v�̃A�C�e����\������X�N���v�g
/// </summary>
public class SpellPreview : MonoBehaviour
{
    [SerializeField]
    List<SO_Spell> spell = new List<SO_Spell>(); //�V���b�v�̃A�C�e���̃��X�g

    //�A�C�e�����Ƃ̉摜�A���O�A���i�A�I�����ꂽ����p�̘g��ݒ肷��ϐ�
    [SerializeField] Image[] images = new Image[8];
    [SerializeField] TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[8];
    [SerializeField] TextMeshProUGUI[] priceTexts = new TextMeshProUGUI[8];
    [SerializeField] GameObject[] selectedFrames = new GameObject[8];
    void Start()
    {
        if (images != null)
        {
            //���ꂼ��̕ϐ��ɃA�C�e����ScriptableObject�̒��g����
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = spell[i].SpellSprite;
                nameTexts[i].text = spell[i].SpellName;
                priceTexts[i].text = spell[i].SpellPrice.ToString() + "yen";
            }
        }
    }


    /// <summary>
    /// �I�����ꂽ���Ƃ��킩��₷������t���[����\��
    /// </summary>
    /// <param name="gameObject"></param>
    public void ShowSelectedFrame(GameObject gameObject)
    {
        foreach (GameObject g in selectedFrames)
        {
            g.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    /// <summary>
    /// �I�����ꂽ�t���[�����\���ɂ��鏈��
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
