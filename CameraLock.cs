using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// カメラをロックするためのスクリプト
/// </summary>
public class CameraLock : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;

    private float originalXAxisValue;
    private float originalYAxisValue;

    private void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }
    }

    /// <summary>
    /// 視点移動をロック
    /// </summary>
    public void LockCameraRotation()
    {
        // 現在の値を保存
        originalXAxisValue = freeLookCamera.m_XAxis.Value;
        originalYAxisValue = freeLookCamera.m_YAxis.Value;

        // 値を固定して視点移動を無効化
        freeLookCamera.m_XAxis.Value = originalXAxisValue;
        freeLookCamera.m_YAxis.Value = originalYAxisValue;

        // 追加で、されないようにする
        freeLookCamera.m_XAxis.m_InputAxisName = "";
        freeLookCamera.m_YAxis.m_InputAxisName = "";
    }

    /// <summary>
    /// 視点移動ロックを解除
    /// </summary>
    public void UnlockCameraRotation()
    {
        // ロック解除
        freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X"; // または適切な入力名
        freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y"; // または適切な入力名

        // もとの回転状態に戻す
        freeLookCamera.m_XAxis.Value = originalXAxisValue;
        freeLookCamera.m_YAxis.Value = originalYAxisValue;
    }
}
