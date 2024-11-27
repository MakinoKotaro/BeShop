using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
/// <summary>
/// タイトル画面のUIスクリプト
/// </summary>
public class TitleUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image titlePanelImage;
    [SerializeField] private TextMeshProUGUI pressAnyKeyText;


    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadePressAnyKeyDuration;
    [SerializeField] private float color_a;

    [SerializeField] private float popTitleTime;
    [SerializeField] private float popOtherTime;

    void Start()
    {
        //pressAnyKeyText = GetComponent<TextMeshProUGUI>();

        Invisible();

        DOVirtual.DelayedCall(popTitleTime, () =>
        {
            TitleFadeIn();
        });

        DOVirtual.DelayedCall(popOtherTime, () =>
        {
            OtherFadeIn();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
