using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 敵：クマのスクリプト
/// </summary>
public class Bear : Enemy
{
    [SerializeField] private GameObject enemyManagerObj;
    private EnemyManager _enemyManager;


    [SerializeField] private float bearHealth;
    [SerializeField] private int bearAttackPower;

    [SerializeField] private float bearMoveSpeed = 5f;

    Animator animator;
    //=============各アニメーション=============
    [SerializeField] string idle_animation;
    [SerializeField] string attack_animation;
    [SerializeField] string walk_animation;
    [SerializeField] string dead_animation;
    //==========================================

    private bool playerIsHere = false;
    private GameObject player;

    private float destroyDelay = 0.7f;
    private float distanceToPlayer = 3.0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        health = bearHealth;
        attackPower = bearAttackPower;

        canMove = true;

        player = GameObject.FindWithTag("Player");
        _enemyManager = enemyManagerObj.GetComponent<EnemyManager>();
    }

    void Update()
    {
        
    }

    public override void EnemyMove(bool foundPlayer, Vector3 playerPosition)
    {
        if (canMove == true)
        {
            playerIsHere = foundPlayer;

            if (foundPlayer == true)
            {
                StartCoroutine(Move(playerPosition));
            }
            else
            {
                animator.Play(idle_animation);
            }
        }
    }

    /// <summary>
    /// 攻撃を開始する処理
    /// </summary>
    public override void EnemyAttack()
    {
        StartCoroutine(Attack());
    }

    /// <summary>
    /// 攻撃を止める処理
    /// </summary>
    public override void EnemyStopAttack()
    {
        StopCoroutine(Attack());
    }

    /// <summary>
    /// 攻撃を受ける処理
    /// </summary>
    /// <param name="damageAmount"></param>
    public override void EnemyTakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("now HP ： " +  health);
        if(health <= 0)
        {
            EnemyDead(); 
        }
    }

    /// <summary>
    /// 死んだ処理
    /// </summary>
    public override void EnemyDead()
    {
        animator.Play(dead_animation);
        canMove = false;

        Invoke("DestroyObj", destroyDelay);
    }

    /// <summary>
    /// 削除する処理
    /// </summary>
    void DestroyObj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 移動のコルーチン
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    IEnumerator Move(Vector3 playerPosition)
    {
        while (playerIsHere == true)
        {
            animator.Play(walk_animation);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, bearMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerPosition) < distanceToPlayer) // 距離のしきい値
            {
                break; // 目標位置に到達したらループを抜ける
            }

            yield return null;
        }
        
    }
    /// <summary>
    /// 攻撃のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        animator.Play(attack_animation);

        GameObject sFXManagerObj = GameObject.FindGameObjectWithTag("SFXManager");
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetSwingAttackSound();
        EnemySearchPlayer enemySearchPlayer = GetComponent<EnemySearchPlayer>();

        yield return new WaitForSeconds(0.6f);
        enemySearchPlayer.ChackPlayerFrame();
        if (enemySearchPlayer.PlayerInFront == true)
        {
            DoneDamageToPlayer(bearAttackPower);
        }
        yield return new WaitForSeconds(4.4f);
    }
}
