using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPの可視化スクリプト
/// </summary>
public class HpBarManager : MonoBehaviour
{
    [SerializeField] private Image hpImage; //HPを表示するためのImage

    Color originalColor; //元の色を保存しておく変数
    private GameObject player; //プレイヤーのオブジェクト
    [SerializeField] private PlayerParameter playerParameter; //プレイヤーのパラメータを持っているスクリプト

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
