using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �A�C�e���ɃJ�[�\����������ۂɐ�����\������X�N���v�g
/// </summary>
public class FollowCursor : MonoBehaviour
{

    [SerializeField] RectTransform panel; //�����p�l��
    [SerializeField] Canvas canvas; //�L�����o�X

    private float distanceFromCamera = 10f; //�J��������̋���

    [SerializeField] Vector3 panelOffset; //�p�l���̃I�t�Z�b�g

    private void Start()
    {
        panel = GetComponent<RectTransform>(); //�p�l����RectTransform���擾
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
