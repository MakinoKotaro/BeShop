using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の継承スクリプト
/// </summary>
public abstract class Enemy : MonoBehaviour, IE_Attack, IE_Move, IE_TakeDamage, IE_Dead, IE_StopAttack
{
    protected float health; //体力
    protected float attackPower; //攻撃力

    protected bool canMove; //移動できるかどうか

    GameObject player; //プレイヤー

    public abstract void EnemyMove(bool foundPlayer, Vector3 playerPosition); //敵の移動
    public abstract void EnemyAttack(); //敵の攻撃

    public abstract void EnemyTakeDamage(float damageAmount); //敵がダメージを受ける

    public abstract void EnemyDead(); //敵の死亡

    public abstract void EnemyStopAttack(); //敵の攻撃を止める

    void Start()
    {

    }

    /// <summary>
    /// プレイヤーにダメージを与える処理
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
            Debug.Log($"{damageAmount} のダメージをプレイヤーに与えました。");
        }
    }
}
