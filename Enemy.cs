using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の継承スクリプト
/// </summary>
public abstract class Enemy : MonoBehaviour, IE_Attack, IE_Move, IE_TakeDamage, IE_Dead, IE_StopAttack
{
    protected float health;
    protected float attackPower;

    protected bool canMove;

    GameObject player;

    public abstract void EnemyMove(bool foundPlayer, Vector3 playerPosition);
    public abstract void EnemyAttack();

    public abstract void EnemyTakeDamage(float damageAmount);

    public abstract void EnemyDead();

    public abstract void EnemyStopAttack();

    void Start()
    {
    }
    protected void DoneDamageToPlayer(float damageAmount)
    {
        
        player = GameObject.FindWithTag("Player");
        PlayerParameter playerParameter = player.GetComponent<PlayerParameter>();
        PlayerController playerController = player.GetComponent< PlayerController>();
        if (playerParameter != null && playerController.CanDodge == true)
        {
            playerParameter.PlayerTakeDamage(damageAmount);
            Debug.Log($"{damageAmount} のダメージをプレイヤーに与えました。");
        }
    }
}
