using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 魔法ショップのアイテムを表示するスクリプト
/// </summary>
public class SpellPreview : MonoBehaviour
{
    [SerializeField]
    List<SO_Spell> spell = new List<SO_Spell>(); //ショップのアイテムのリスト

    //アイテムごとの画像、名前、価格、選択された判定用の枠を設定する変数
    [SerializeField] Image[] images = new Image[8];
    [SerializeField] TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[8];
    [SerializeField] TextMeshProUGUI[] priceTexts = new TextMeshProUGUI[8];
    [SerializeField] GameObject[] selectedFrames = new GameObject[8];
    void Start()
    {
        if (images != null)
        {
            //それぞれの変数にアイテムのScriptableObjectの中身を代入
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = spell[i].SpellSprite;
                nameTexts[i].text = spell[i].SpellName;
                priceTexts[i].text = spell[i].SpellPrice.ToString() + "yen";
            }
        }
    }


    /// <summary>
    /// 選択されたことをわかりやすくするフレームを表示
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
    /// 選択されたフレームを非表示にする処理
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
