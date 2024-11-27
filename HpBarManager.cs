using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// HPの可視化スクリプト
/// </summary>
public class HpBarManager : MonoBehaviour
{
    [SerializeField] private Image hpImage;

    Color originalColor;
    private GameObject player;
    [SerializeField] private PlayerParameter playerParameter;

    /// <summary>
    /// 現在のHPを表示
    /// </summary>
    /// <param name="hp"></param>
    public void ShowCurrentHp(float hp)
    {
        if (hpImage == null)
        {
            Debug.LogError("hpImage is missing");
        }
        else
        {
            hpImage.fillAmount = hp;
        }
    }
}
