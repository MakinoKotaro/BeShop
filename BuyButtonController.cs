using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 購入ボタンのコントローラー
/// </summary>
public class BuyButtonController : MonoBehaviour
{
    [SerializeField] GameObject dropdownObject; // ドロップダウンオブジェクト

    public void OnClicked()
    {
        DropdownController dropdownController = dropdownObject.GetComponent<DropdownController>();
        dropdownController.BuyItem();
    }
}
