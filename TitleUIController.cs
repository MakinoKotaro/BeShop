using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
/// <summary>
/// �^�C�g����ʂ�UI�X�N���v�g
/// </summary>
public class TitleUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText; // �^�C�g���̃e�L�X�g
    [SerializeField] private Image titlePanelImage; // �^�C�g���̃p�l��
    [SerializeField] private TextMeshProUGUI pressAnyKeyText; // �C�ӂ̃L�[�������Ă�������


    [SerializeField] private float fadeDuration; // �t�F�[�h�̎���
    [SerializeField] private float fadePressAnyKeyDuration; // �C�ӂ̃L�[�������Ă��������̓_�ł̎���
    [SerializeField] private float color_a; // �����x

    [SerializeField] private float popTitleTime; // �^�C�g����\�����鎞��
    [SerializeField] private float popOtherTime; // ���̑���\�����鎞��

    void Start()
    {
        //pressAnyKeyText = GetComponent<TextMeshProUGUI>();

        Invisible(); // �^�C�g����UI���\���ɂ���

        DOVirtual.DelayedCall(popTitleTime, () =>
        {
            TitleFadeIn();
        });

        DOVirtual.DelayedCall(popOtherTime, () =>
        {
            OtherFadeIn();
        });
    }

    //Update���g�p���Ȃ��Ȃ����

    /// <summary>
    /// �^�C�g����UI���\���ɂ���
    /// </summary>
    private void Invisible()
    {
        if (titleText != null)
        {
            Color color = titleText.color;
            color.a = 0.0f;
            titleText.color = color;
        }

        if (titlePanelImage != null)
        {
            Color color = titlePanelImage.color;
            color.a = 0.0f;
            titlePanelImage.color = color;
        }

        if (pressAnyKeyText != null)
        {
            Color color = pressAnyKeyText.color;
            color.a = 0.0f;
            pressAnyKeyText.color = color;
        }

    }

    /// <summary>
    /// �^�C�g���̃e�L�X�g�����X�ɕ\�����鏈��
    /// </summary>
    void TitleFadeIn()
    {
        if(titleText != null)
        {
            titleText.DOFade(1.0f, fadeDuration);
        }
        if(titlePanelImage != null)
        {
            titlePanelImage.DOFade(color_a, fadeDuration);
        }
    }

    /// <summary>
    /// �e�L�X�g�ȊO�����X�ɕ\�����鏈��
    /// </summary>
    void OtherFadeIn()
    {
        pressAnyKeyText.DOFade(1.0f, fadePressAnyKeyDuration).SetLoops(-1, LoopType.Yoyo);

    }
}
