using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownController : MonoBehaviour
{
    [SerializeField] SO_ShopItem knife;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDesc;
    [SerializeField] Image itemImage;
    TMP_Dropdown dropdown;

    private bool isBought = false;

    public bool IsBought { get => isBought; set => isBought = value; }

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    public void OnSelected()
    {
        switch(dropdown.value)
        {
            case 0:
                itemName.text = knife.GetItemName();
                itemDesc.text = knife.GetItemDesc();
                itemImage.sprite = knife.GetItemSprite();
                
                if(isBought == true)
                {
                    
                }
                break;
            default:
                itemName.text = "アイテム名";
                itemDesc.text = "アイテム説明";
                itemImage.sprite = null; 
                break;
        }
    }


    public void BuyItem()
    {
        isBought = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
