using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
/// <summary>
/// タイトル画面のUIスクリプト
/// </summary>
public class TitleUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText; // タイトルのテキスト
    [SerializeField] private Image titlePanelImage; // タイトルのパネル
    [SerializeField] private TextMeshProUGUI pressAnyKeyText; // 任意のキーを押してください


    [SerializeField] private float fadeDuration; // フェードの時間
    [SerializeField] private float fadePressAnyKeyDuration; // 任意のキーを押してくださいの点滅の時間
    [SerializeField] private float color_a; // 透明度

    [SerializeField] private float popTitleTime; // タイトルを表示する時間
    [SerializeField] private float popOtherTime; // その他を表示する時間

    void Start()
    {
        //pressAnyKeyText = GetComponent<TextMeshProUGUI>();

        Invisible(); // タイトルのUIを非表示にする

        DOVirtual.DelayedCall(popTitleTime, () =>
        {
            TitleFadeIn();
        });

        DOVirtual.DelayedCall(popOtherTime, () =>
        {
            OtherFadeIn();
        });
    }

    //Updateを使用しないなら消す

    /// <summary>
    /// タイトルのUIを非表示にする
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
    /// タイトルのテキストを徐々に表示する処理
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
    /// テキスト以外を徐々に表示する処理
    /// </summary>
    void OtherFadeIn()
    {
        pressAnyKeyText.DOFade(1.0f, fadePressAnyKeyDuration).SetLoops(-1, LoopType.Yoyo);

    }
}
