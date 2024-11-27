using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private Transform player; // PlayerのTransformをInspectorで設定
    [SerializeField] private CinemachineFreeLook freeLookCamera; // CinemachineのFreeLookCameraをInspectorで設定

    void Update()
    {
        if (freeLookCamera != null && player != null)
        {
            // カメラの正面方向を取得
            Vector3 cameraForward = freeLookCamera.transform.forward;
            cameraForward.y = 0; // Y成分をゼロにして地面平面に沿う方向にする
            cameraForward.Normalize(); // 正規化

            // Playerの向きをカメラの正面方向に合わせる
            if (cameraForward != Vector3.zero) // ゼロベクトルを避ける
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
                player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }
}
