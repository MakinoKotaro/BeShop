using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �J�[�\����\���ƁA�����Ƀ��b�N����X�N���v�g
/// </summary>
public class MouseCursorController : MonoBehaviour
{
    [SerializeField] private bool cursorLock = false; //���b�N���邩�ǂ�����bool
    LoadScene loadScene; //�V�[���J�ڂ̃X�N���v�g

/// <summary>
/// �J�[�\�����b�N�̃v���p�e�B
/// </summary>
    public bool CursorLock
    {
        get { return cursorLock; }
        set { cursorLock = value; }
    }
    
    private void Start()
    {
        loadScene = FindAnyObjectByType<LoadScene>();

        //���b�N����ꍇ�A�ŏ��ɔ�\���ɂ���
        if (cursorLock == true)
        {
            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        
        //���b�N����ꍇ�A�����ɌŒ肷��
        if (cursorLock == true)
        {
            CursorStayCenter();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    /// <summary>
    /// �����ɃJ�[�\����u������
    /// </summary>
    void CursorStayCenter()
    {
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.ForceSoftware);
    }


    /// <summary>
    /// �J�[�\�������b�N���A�����ɌŒ肷�邽�߂̏���
    /// </summary>
    public void LockAndStayCenter()
    {
        // �J�[�\�����\���ɂ���
        Cursor.visible = false;

        // �J�[�\�������b�N
        Cursor.lockState = CursorLockMode.Locked;

        // MouseCursorController �� cursorLock �t���O���X�V
        CursorLock = true;  // ���b�N��Ԃ� true �ɂ��Ă���

        // �J�[�\���ʒu�𒆉��Ƀ��b�N
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.ForceSoftware);
    }
}
