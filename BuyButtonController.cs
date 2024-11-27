using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtonController : MonoBehaviour
{
    [SerializeField] GameObject dropdownObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        DropdownController dropdownController = dropdownObject.GetComponent<DropdownController>();
        dropdownController.BuyItem();
    }
}
