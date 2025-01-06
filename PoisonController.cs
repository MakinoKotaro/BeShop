using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ł̋������Ǘ����鏈��
/// </summary>
public class PoisonController : MonoBehaviour
{
    [SerializeField] private GameObject mushParent; // �e�I�u�W�F�N�g
    Mushroom mushroom; // Mushroom�X�N���v�g

    private BoxCollider poisonCollider; // �ł̃R���C�_�[
    private GameObject player; // �v���C���[
    
    void Start()
    {
        poisonCollider = GetComponent<BoxCollider>();
        poisonCollider.enabled = false;
        mushroom = mushParent.GetComponent<Mushroom>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(mushroom.CanShotPoison == false)
        {
            poisonCollider.enabled = true;
        }
        else
        {
            poisonCollider.enabled = false;
        }
    }

    /// <summary>
    /// �ł��v���C���[�ɐڐG���Ă���Ƃ��̏���
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mushroom = mushParent.GetComponent<Mushroom>();
            if (mushroom.CanShotPoison == true)
            {
                Debug.Log("canShotPoison");
                
            }
            else
            {
                PlayerParameter playerParameter = player.GetComponent<PlayerParameter>();
                playerParameter.PlayerTakePoison();
                playerParameter.TakePoison = true;
            }
        }
    }
}
