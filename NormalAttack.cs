using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 通常攻撃のスクリプト
/// </summary>
public class NormalAttack : MagicBase
{
    [SerializeField] private SO_Spell spell; //SO_Spellを取得

    private Vector3 targetPointPosition; // 魔法が着地する場所の位置を取得
    private List<GameObject> activeSpells = new List<GameObject>();
    private GameObject currentSpell; // 発射された魔法を入れるもの

    [SerializeField] float moveSpeed = 5f; // 魔法の速度
    //[SerializeField] private float spellRange = 10f;

    [SerializeField] GameObject player; // Playerを取得
    private PlayerController playerController; // PlayerControllerを取得

    GameObject castPoint; // 魔法を発射する場所
    
    Vector3 screenCenter; // 画面の中心
    Vector3 bulletDirection; // 魔法の方向

    [SerializeField] private GameObject particleManagerObj; // ParticleManagerのオブジェクト
    private GameObject sFXManagerObj; // SFXManagerのオブジェクト

    private float spellLength = 10.0f; // 魔法の長さ
    private float moveLength = 0.5f; // 魔法の移動距離
    private float destroyDelay = 0.7f; // 魔法の消滅までの時間
    
    // NormalAttackのコンストラクタ
    public NormalAttack()
    {
        ManaCost = 5;  //これ定数にするか変数にするかにしてください
        MagicDamage = 5; //これ定数にするか変数にするかにしてください
    }

    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // player内のPlayerControllerを取得

        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0).normalized;
        //マジックナンバー発見！コメント追加などしてください

        sFXManagerObj = GameObject.FindWithTag("SFXManager");
    }

/// <summary>
/// 魔法の挙動
/// </summary>
/// <param name="targetPoint"></param>
    public override void Behaviour(Vector3 targetPoint)
    {

        castPoint = GameObject.FindWithTag("CastPoint");
        //DOTweenで魔法の挙動を処理
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 targetPosition = cameraPosition + cameraForward * spellLength;

        bulletDirection = targetPosition - castPoint.transform.position;

        currentSpell.transform.DOLocalMove(bulletDirection, moveLength).SetRelative(true).SetEase(Ease.Linear);
        Invoke("DestroySpell", destroyDelay);
    }

/// <summary>
/// 魔法の発射
/// </summary>
/// <param name="castPoint"></param>
    public override void Cast(Transform castPoint)
    {
        SpendMana("通常攻撃", ManaCost);

        // 発射時のtargetPointの位置を取得
        targetPointPosition = playerController.TargetPoint01.position; // PlayerのtargetPointを取得

        // 魔法を発射
        currentSpell = Instantiate(spell.SpellPrefab, castPoint.position, Quaternion.identity);

        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetShotSound();
    }

/// <summary>
/// 魔法を消す処理
/// </summary>
    void DestroySpell()
    {
        Destroy(currentSpell);
    }



    private void OnCollisionEnter(Collision collision)
    {

        //string collidedObjectName = collision.gameObject.name;

        //// 衝突したオブジェクトの名前を表示
        //Debug.Log("衝突したオブジェクトの名前: " + collidedObjectName);


        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");

            IE_TakeDamage e_TakeDamage = collision.gameObject.GetComponent<IE_TakeDamage>();

            e_TakeDamage.EnemyTakeDamage(MagicDamage);

            ContactPoint contact = collision.contacts[0];

            // 衝突点の位置（x, y, z）
            Vector3 collisionPoint = contact.point;

            DestroySpell();
        }
    }
}

