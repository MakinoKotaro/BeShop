using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using DG.Tweening.Plugins.Options;

/// <summary>
/// SHOPの文字を動かす処理
/// </summary>
public class FontTweening : MonoBehaviour
{
    TextMeshPro text; // TextMeshProUGUIを入れる変数

    private float maxFontSize = 13.0f; // フォントサイズの最大値
    private float minFontSize = 10.0f; // フォントサイズの最小値
    void Start()
    {
        text = GetComponent<TextMeshPro>();


        var _sequence = DOTween.Sequence();

        //どういう処理しているか簡単に説明
        _sequence.Append(DOTween.To(() => text.fontSize, x => text.fontSize = x, maxFontSize, 1f)) 
                 .Append(DOTween.To(() => text.fontSize, x => text.fontSize = x, minFontSize, 1f)) 
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
