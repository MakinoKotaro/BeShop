using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HP�̉����X�N���v�g
/// </summary>
public class HpBarManager : MonoBehaviour
{
    [SerializeField] private Image hpImage; //HP��\�����邽�߂�Image

    Color originalColor; //���̐F��ۑ����Ă����ϐ�
    private GameObject player; //�v���C���[�̃I�u�W�F�N�g
    [SerializeField] private PlayerParameter playerParameter; //�v���C���[�̃p�����[�^�������Ă���X�N���v�g

    /// <summary>
    /// ���݂�HP��\��
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
