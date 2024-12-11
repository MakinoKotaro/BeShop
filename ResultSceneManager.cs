using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// リザルト画面のUIを管理するスクリプト
/// </summary>
public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] private Image resultPanelImage; //リザルト画面のパネルを入れるもの
    [SerializeField] private TextMeshProUGUI resultText; //リザルト画面のテキストを入れるもの

    private float delayTime = 4f; //フェードインまでの待ち時間
    [SerializeField] private float fadeDuration = 3f; //フェードインの時間

 
    void Start()
    {
        Invisible(transform);

        DOVirtual.DelayedCall(delayTime, () =>
        {
            FadeIn();
        });
    }

    /// <summary>
    /// パネルとテキストを非表示にする
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
    /// パネルとテキストをフェードインする
    /// </summary>
    void FadeIn()
    {
        // パネルの透明度をフェードイン
        if (resultPanelImage != null)
        {
            resultPanelImage.DOFade(0.8f, fadeDuration);
        }
        // テキスト（Text）の透明度をフェードイン
        if (resultText != null)
        {
            resultText.DOFade(1f, fadeDuration);
        }
    }
}
