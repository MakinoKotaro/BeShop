using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w���{�^���̃R���g���[���[
/// </summary>
public class BuyButtonController : MonoBehaviour
{
    [SerializeField] GameObject dropdownObject; // �h���b�v�_�E���I�u�W�F�N�g

    public void OnClicked()
    {
        DropdownController dropdownController = dropdownObject.GetComponent<DropdownController>();
        dropdownController.BuyItem();
    }
}
