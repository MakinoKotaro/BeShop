using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵：キノコのスクリプト
/// </summary>
public class Mushroom : Enemy, IE_SpecialMove
{
    [SerializeField] private GameObject enemyManagerObj; //敵の管理スクリプトのオブジェクト
    private EnemyManager _enemyManager; //敵の管理スクリプト


    [SerializeField] private float mushroomHealth; //キノコの体力
    [SerializeField] private int mushroomAttackPower; //キノコの攻撃力

    [SerializeField] private float mushroomMoveSpeed = 5f; //キノコの移動速度

    Animator animator;
    //=============各アニメーション=============
    [SerializeField] string idle_animation;
    [SerializeField] string attack_animation;
    [SerializeField] string walk_animation;
    [SerializeField] string dead_animation;
    //==========================================

    private GameObject player; //プレイヤーのオブジェクト
    private bool playerIsHere = false; //プレイヤーがいるかどうか

    private float destroyDelay = 1.8f; //破壊されるまでの時間
    private float attackInterval = 5.0f; //攻撃間隔
    private float popPoisonDelay = 0.5f; //毒のエフェクトを出すまでの時間
    private float distanceToPlayer = 3.0f; //プレイヤーとの距離のしきい値

    //特殊行動用の変数たち
    [SerializeField] private ParticleSystem poisonParticle; //毒のエフェクト
    PoisonController poisonController; //毒のコントローラー
    private bool canShootPoison = true; //毒を出すことが可能かどうか

    public bool CanShootPoison { get => canShootPoison; set => canShootPoison = value; }

    void Start()
    {
        animator = GetComponent<Animator>();
        health = mushroomHealth;
        attackPower = mushroomAttackPower;

        canMove = true;

        player = GameObject.FindWithTag("Player");
        _enemyManager = enemyManagerObj.GetComponent<EnemyManager>();
        ParticleSystem poisonParticle = GetComponent<ParticleSystem>();

        poisonController = GetComponent<PoisonController>();
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    /// <param name="foundPlayer"></param>
    /// <param name="playerPosition"></param>
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
    /// 攻撃の処理
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
    /// 敵が攻撃を受ける処理
    /// </summary>
    /// <param name="damageAmount"></param>
    public override void EnemyTakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("now HP ： " + health);
        if (health <= 0)
        {
            EnemyDead();
        }
    }


    /// <summary>
    /// 敵が死ぬ処理
    /// </summary>
    public override void EnemyDead()
    {
        animator.Play(dead_animation);
        canMove = false;

        Invoke("DestroyObj", destroyDelay);
    }

    /// <summary>
    /// 敵を削除する処理
    /// </summary>
    void DestroyObj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 敵の特別な動き（毒を吐く）処理
    /// </summary>
    public void EnemySpecialMove()
    {
        canShootPoison = false;
        poisonParticle.Play();
        StartCoroutine(SpecialAttack());
    }

    /// <summary>
    /// 移動コルーチン
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    IEnumerator Move(Vector3 playerPosition)
    {
        while (playerIsHere == true)
        {
            animator.Play(walk_animation);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, mushroomMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerPosition) < distanceToPlayer) // 距離のしきい値
            {
                break; // 目標位置に到達したらループを抜ける
            }

            yield return null;
        }

    }

    /// <summary>
    /// 攻撃コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        animator.Play(attack_animation);
        Invoke("EnemySpecialMove", popPoisonDelay);
        GameObject sFXManagerObj = GameObject.FindGameObjectWithTag("SFXManager");
        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetSwingAttackSound();
        
        yield return new WaitForSeconds(attackInterval);
    }

    /// <summary>
    /// 特別な行動（毒吐き）コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(1.0f);
        canShootPoison = true;
    }
}
