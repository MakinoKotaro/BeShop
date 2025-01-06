using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ���U���g��ʂ�UI���Ǘ�����X�N���v�g
/// </summary>
public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] private Image resultPanelImage; //���U���g��ʂ̃p�l�����������
    [SerializeField] private TextMeshProUGUI resultText; //���U���g��ʂ̃e�L�X�g���������

    private float delayTime = 4f; //�t�F�[�h�C���܂ł̑҂�����
    [SerializeField] private float fadeDuration = 3f; //�t�F�[�h�C���̎���

 
    void Start()
    {
        Invisible(transform);

        DOVirtual.DelayedCall(delayTime, () =>
        {
            FadeIn();
        });
    }

    /// <summary>
    /// �p�l���ƃe�L�X�g���\���ɂ���
    /// </summary>
    /// <param name="parent"></param>
    void Invisible(Transform parent)
    {
        if (resultPanelImage != null)
        {
            Color color = resultPanelImage.color;
            color.a = 0f;
            resultPanelImage.color = color;
        }

        if (resultText != null)
        {
            Color color = resultText.color;
            color.a = 0f;
            resultText.color = color;
        }
    }

    /// <summary>
    /// �p�l���ƃe�L�X�g���t�F�[�h�C������
    /// </summary>
    void FadeIn()
    {
        // �p�l���̓����x���t�F�[�h�C��
        if (resultPanelImage != null)
        {
            resultPanelImage.DOFade(0.8f, fadeDuration);
        }
        // �e�L�X�g�iText�j�̓����x���t�F�[�h�C��
        if (resultText != null)
        {
            resultText.DOFade(1f, fadeDuration);
        }
    }
}
