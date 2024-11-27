using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ショップ画面に表示されているアイテムにカーソルが乗ったときに表示する説明のUIを管理するスクリプト
/// </summary>
public class ShopUiManager : MonoBehaviour
{
    [SerializeField] private Image panelImage; //アイテム説明のパネルを入れるもの

    [SerializeField] private TextMeshProUGUI itemNameText, itemDescText; //パネル内のアイテム名と説明をいれるもの

    [SerializeField] private Button buyButton;

    private PurchaseManager purchaseManager;

    [SerializeField] private TextMeshProUGUI playerInfoText_attackPower;
    [SerializeField] private TextMeshProUGUI playerInfoText_hitPoint;
    [SerializeField] private TextMeshProUGUI playerInfoText_speed;
    [SerializeField] private GameObject playerParameterObj;
    PlayerParameter playerParameter;

    private void Start()
    {
        panelImage.GetComponent<Image>(); //panelImage内のImageコンポーネントを取得
        

        itemNameText = itemNameText.GetComponent<TextMeshProUGUI>(); // itemNameText内のTextMeshProコンポーネントを取得
        
        itemDescText = itemDescText.GetComponent<TextMeshProUGUI>(); // itemDescText内のTextMeshProコンポーネントを取得
        if (playerInfoText_attackPower != null)
        {
            playerInfoText_attackPower = playerInfoText_attackPower.GetComponent<TextMeshProUGUI>();
        }
        purchaseManager = FindObjectOfType<PurchaseManager>(); //PurchaseManagerが入っているオブジェクトを探す

        playerParameter = playerParameterObj.GetComponent<PlayerParameter>();

        HideDescUi(); //最初は、説明UIを隠す
    }

    /// <summary>
    /// 説明UIを表示する処理
    /// </summary>
    /// <param name="shopItem"></param>
    public void ShowDescUi(SO_ShopItem shopItem)
    {

        itemNameText.text = shopItem.ItemName;
        itemDescText.text = shopItem.ItemDesc;

        Color panelColor = panelImage.color;
        panelColor.a = 1;
        panelImage.color = panelColor;

        Color itemNameColor = itemNameText.color;
        itemNameColor.a = 1;
        itemNameText.color = itemNameColor;

        Color itemDescColor = itemDescText.color;
        itemDescColor.a = 1;
        itemDescText.color = itemDescColor;
    }

    /// <summary>
    /// 魔法の説明UIを表示する処理
    /// </summary>
    /// <param name="shopSpell"></param>
    public void ShowSpellDescUi(SO_Spell shopSpell)
    {
        itemNameText.text = shopSpell.SpellName;
        itemDescText.text = shopSpell.SpellDesc;

        Color panelColor = panelImage.color;
        panelColor.a = 1;
        panelImage.color = panelColor;

        Color itemNameColor = itemNameText.color;
        itemNameColor.a = 1;
        itemNameText.color = itemNameColor;

        Color itemDescColor = itemDescText.color;
        itemDescColor.a = 1;
        itemDescText.color = itemDescColor;
    }

    /// <summary>
    /// 説明UIを非表示にする処理
    /// </summary>
    public void HideDescUi()
    {
        Color panelColor = panelImage.color;
        panelColor.a = 0;
        panelImage.color = panelColor;

        Color itemNameColor = itemNameText.color;
        itemNameColor.a = 0;
        itemNameText.color = itemNameColor;

        Color itemDescColor = itemDescText.color;
        itemDescColor.a = 0;
        itemDescText.color = itemDescColor;
    }

    private void Update()
    {
        if (playerInfoText_attackPower != null)
        {
            playerInfoText_attackPower.text = "Damage: \n" + playerParameter.PlayerPower.ToString();
            playerInfoText_hitPoint.text = "HP: \n" + playerParameter.PlayerHitPoint.ToString();
            playerInfoText_speed.text = "Speed: \n" + playerParameter.PlayerSpeed.ToString();
        }
    }
}


