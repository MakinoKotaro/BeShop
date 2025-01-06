using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using DG.Tweening.Plugins.Options;

/// <summary>
/// SHOP�̕����𓮂�������
/// </summary>
public class FontTweening : MonoBehaviour
{
    TextMeshPro text; // TextMeshProUGUI������ϐ�

    private float maxFontSize = 13.0f; // �t�H���g�T�C�Y�̍ő�l
    private float minFontSize = 10.0f; // �t�H���g�T�C�Y�̍ŏ��l
    void Start()
    {
        text = GetComponent<TextMeshPro>();


        var _sequence = DOTween.Sequence();

        //�ǂ������������Ă��邩�ȒP�ɐ���
        _sequence.Append(DOTween.To(() => text.fontSize, x => text.fontSize = x, maxFontSize, 1f)) 
                 .Append(DOTween.To(() => text.fontSize, x => text.fontSize = x, minFontSize, 1f)) 
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
