using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IceMagic : MagicBase
{
    [SerializeField] private SO_Spell spell; //SO_Spellを取得

    private Vector3 targetPointPosition; // 魔法が着地する場所の位置を取得
    private List<GameObject> activeSpells = new List<GameObject>();
    private GameObject currentSpell; // 発射された魔法を入れるもの

    [SerializeField] float moveSpeed = 5f; // 魔法の速度
    //[SerializeField] private float spellRange = 10f;

    [SerializeField] GameObject player; // Playerを取得
    private PlayerController playerController; // PlayerControllerを取得

    GameObject castPoint;

    Vector3 screenCenter;
    Vector3 bulletDirection;

    [SerializeField] private GameObject particleManagerObj;
    private GameObject sFXManagerObj;

    private float spellLength = 10.0f;
    private float moveLength = 0.5f;
    private float destroyDelay = 0.7f;
    // IceMagicのコンストラクタ
    public IceMagic()
    {
        ManaCost = 5;
        MagicDamage = 5;
    }

    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // player内のPlayerControllerを取得

        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0).normalized;

        sFXManagerObj = GameObject.FindWithTag("SFXManager");
    }

    void Update()
    {

    }

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
