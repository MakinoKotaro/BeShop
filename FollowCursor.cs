using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムにカーソルが乗った際に説明を表示するスクリプト
/// </summary>
public class FollowCursor : MonoBehaviour
{

    [SerializeField] RectTransform panel; //説明パネル
    [SerializeField] Canvas canvas; //キャンバス

    private float distanceFromCamera = 10f; //カメラからの距離

    [SerializeField] Vector3 panelOffset; //パネルのオフセット

    private void Start()
    {
        panel = GetComponent<RectTransform>(); //パネルのRectTransformを取得
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; 

        mousePosition.z = distanceFromCamera;
        
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Debug.Log(worldPosition);

        panel.position = worldPosition + panelOffset;
    }
}
