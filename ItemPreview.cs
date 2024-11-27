using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ショップ内にアイテムの情報を表示する処理
/// </summary>
public class ItemPreview : MonoBehaviour
{
    [SerializeField]
    List<SO_ShopItem> items = new List<SO_ShopItem>(); //ショップのアイテムのリスト

    //アイテムごとの画像、名前、価格、選択された判定用の枠を設定する変数
    [SerializeField] Image[] images = new Image[8];
    [SerializeField] TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[8];
    [SerializeField] TextMeshProUGUI[] priceTexts = new TextMeshProUGUI[8];
    [SerializeField] GameObject[] selectedFrames = new GameObject[8];
    void Start()
    {
        //それぞれの変数にアイテムのScriptableObjectの中身を代入
        for(int i = 0; i < images.Length; i++)
        {
            images[i].sprite = items[i].ItemSprite;
            nameTexts[i].text = items[i].ItemName;
            priceTexts[i].text = items[i].ItemPrice.ToString() + "yen";
        }
    }


    /// <summary>
    /// 選択されたことをわかりやすくするフレームを表示
    /// </summary>
    /// <param name="gameObject"></param>
    public void ShowSelectedFrame(GameObject gameObject)
    {
        foreach(GameObject g in selectedFrames)
        {
            g.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 選択されたフレームを非表示にする
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
