using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
/// <summary>
/// 魔法を選択するUIを表示するスクリプト
/// </summary>
public class SelectSpellUiController : MonoBehaviour
{
    [SerializeField] private RectTransform panel; // 親パネル
    [SerializeField] private float duration = 0.5f; // アニメーションの時間
    [SerializeField] private float targetScale = 0.3f; // 最終的なスケール

    MouseCursorController mouseCursorController;
    void Start()
    {
        mouseCursorController = GameObject.FindWithTag("MainCamera").GetComponent<MouseCursorController>();
    }

    private void Update()
    {
        //Eキーを押されるとUIを表示し、カーソル有効
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // 初期状態ではスケールを小さく設定しておく（画面中央で縮小状態）
            panel.localScale = Vector3.zero;

            // 親パネルのスケールを拡大する
            panel.DOScale(targetScale, duration).SetEase(Ease.OutBack);
            mouseCursorController.CursorLock = false;
        }

        //Eキーを離すとUIを非表示にし、カーソル無効
        else if(Keyboard.current.eKey.wasReleasedThisFrame)
        {
            // 親パネルのスケールを拡大する
            panel.DOScale(0.0f, duration).SetEase(Ease.OutBack);
            
            mouseCursorController.LockAndStayCenter();
        }
    }
}
