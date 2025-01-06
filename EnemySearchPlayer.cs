using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーを見つけるスクリプト
/// </summary>
public class EnemySearchPlayer : MonoBehaviour
{
    [SerializeField] private float radius; //捜索半径
    [SerializeField] private Enemy enemy; //敵のスクリプト

    private float loopTime = 0.01f; //ループのクールタイム
    [SerializeField] private float attackLoopTime = 3f; //攻撃のクールタイム

    private bool canAttack = true; //攻撃可能かどうか

    [SerializeField] private float attackDistance; //攻撃距離
    [SerializeField] private float rayDistance; //レイの距離
    private string targetTag = "Player"; //ターゲットのタグ

    private bool playerInFront = false; //プレイヤーが正面にいるかどうか


    Vector3 playerPosition = Vector3.zero; //プレイヤーの位置

    public bool PlayerInFront { get => playerInFront; set => playerInFront = value; }

    void Start()
    {
        StartCoroutine(SearchPlayer());
    }

    private void Update()
    {
        //Debug.Log(canAttack);
        if (canAttack == true)
        {
            canAttack = false;
            StartCoroutine(AttackPlayer());
        }
        // デバッグ用にレイを可視化する
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
    }

    /// <summary>
    /// プレイヤー捜索コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator SearchPlayer()
    {
        while (true) // 無限ループになりかねないので、ちゃんとフラグとか用意した方がいい
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            bool playerInRange = false;

            // 取得したコライダーをループ
            foreach (Collider collider in hitColliders)
            {
                // タグが"Player"の場合
                if (collider.CompareTag("Player"))
                {
                    playerInRange = true;
                    playerPosition = collider.transform.position; // プレイヤーの位置を取得

                    Vector3 directionToPlayer = (collider.transform.position - transform.position).normalized;
                    // Y軸を除外して向きを設定
                    directionToPlayer.y = 0; // Y成分をゼロにする
                    if (directionToPlayer != Vector3.zero) // ゼロベクターをチェック
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                        transform.rotation = targetRotation;
                    }
                }
            }

            // プレイヤーが範囲内にいるかどうかで処理を変える
            if (playerInRange)
            {
                //Debug用のコードならばUnity_EDITORを使うべきです。
                #if UNITY_EDITOR
                //Debug.Log("Player is here：" + playerPosition);
                #endif

                if (enemy != null)
                {
                    enemy.EnemyMove(true, playerPosition);
                }
                else
                {
                    Debug.LogWarning("Enemy reference is null!");
                }
                //enemy.EnemyMove(true, playerPosition);
            }
            else
            {
                //Debug.Log("Player is not here");

                enemy.EnemyMove(false, /*Vector3.zero*/playerPosition);
            }

            yield return new WaitForSeconds(loopTime);
        }
    }

    /// <summary>
    /// 敵の攻撃の処理を呼び出すかを判定する処理
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackPlayer()
    {
        

        // オブジェクトの正面方向にレイを飛ばす
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            // 当たったオブジェクトのタグをチェック
            if (hit.collider.CompareTag(targetTag) && enemy != null)
            { 
                enemy.EnemyAttack();
                // レイが指定したタグのオブジェクトに当たったときの処理
                Debug.Log("Hit: " + hit.collider.name);
            }
            else if(hit.collider.CompareTag(targetTag) && gameObject.tag == "Turret")
            {
                Golem golem = gameObject.GetComponent<Golem>();
                golem.Attack();
            }
        }

        yield return new WaitForSeconds(attackLoopTime);

        canAttack = true;
    }

    /// <summary>
    /// 攻撃判定を出したフレーム時、目の前にプレイヤーがいるかどうかを判定
    /// </summary>
    public void ChackPlayerFrame()
    {
        // オブジェクトの正面方向にレイを飛ばす
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            // 当たったオブジェクトのタグをチェック
            if (hit.collider.CompareTag(targetTag))
            {
                playerInFront = true;
            }
            else if (hit.collider == null)
            {
                playerInFront = false;
            }
        }
    }

}
