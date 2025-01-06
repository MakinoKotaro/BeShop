using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�̌p���X�N���v�g
/// </summary>
public abstract class Enemy : MonoBehaviour, IE_Attack, IE_Move, IE_TakeDamage, IE_Dead, IE_StopAttack
{
    protected float health; //�̗�
    protected float attackPower; //�U����

    protected bool canMove; //�ړ��ł��邩�ǂ���

    GameObject player; //�v���C���[

    public abstract void EnemyMove(bool foundPlayer, Vector3 playerPosition); //�G�̈ړ�
    public abstract void EnemyAttack(); //�G�̍U��

    public abstract void EnemyTakeDamage(float damageAmount); //�G���_���[�W���󂯂�

    public abstract void EnemyDead(); //�G�̎��S

    public abstract void EnemyStopAttack(); //�G�̍U�����~�߂�

    void Start()
    {

    }

    /// <summary>
    /// �v���C���[�Ƀ_���[�W��^���鏈��
    /// </summary>
    /// <param name="damageAmount"></param>
    protected void DoneDamageToPlayer(float damageAmount)
    {
        
        player = GameObject.FindWithTag("Player");
        PlayerParameter playerParameter = player.GetComponent<PlayerParameter>();
        PlayerController playerController = player.GetComponent< PlayerController>();
        if (playerParameter != null && playerController.CanDodge == true)
        {
            playerParameter.PlayerTakeDamage(damageAmount);
            Debug.Log($"{damageAmount} �̃_���[�W���v���C���[�ɗ^���܂����B");
        }
    }
}
