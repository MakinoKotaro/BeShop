using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// カーソル非表示と、中央にロックするスクリプト
/// </summary>
public class MouseCursorController : MonoBehaviour
{
    [SerializeField] private bool cursorLock = false; //ロックするかどうかのbool
    LoadScene loadScene;

    public bool CursorLock
    {
        get { return cursorLock; }
        set { cursorLock = value; }
    }
    
    private void Start()
    {
        loadScene = FindAnyObjectByType<LoadScene>();

        //ロックする場合、最初に非表示にする
        if (cursorLock == true)
        {
            Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        
        //ロックする場合、中央に固定する
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
    /// 中央にカーソルを置く処理
    /// </summary>
    void CursorStayCenter()
    {
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.ForceSoftware);
    }


    /// <summary>
    /// カーソルをロックし、中央に固定するための処理
    /// </summary>
    public void LockAndStayCenter()
    {
        // カーソルを非表示にする
        Cursor.visible = false;

        // カーソルをロック
        Cursor.lockState = CursorLockMode.Locked;

        // MouseCursorController で cursorLock フラグを更新
        CursorLock = true;  // ロック状態を true にしている

        // カーソル位置を中央にロック
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.ForceSoftware);
    }
}
