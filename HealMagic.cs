using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回復魔法のスクリプト
/// </summary>
public class HealMagic : MagicBase
{
    [SerializeField] private SO_Spell spell; //SO_Spellを取得

    private Vector3 targetPointPosition; // 魔法が着地する場所の位置を取得
    private List<GameObject> activeSpells = new List<GameObject>()； // 発射された魔法を入れるもの
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
    
    // Healのコンストラクタ
    public HealMagic()
    {
        ManaCost = 0;
        MagicDamage = 2.0f;
    }

    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // player内のPlayerControllerを取得

        sFXManagerObj = GameObject.FindWithTag("SFXManager");
    }
    public override void Behaviour(Vector3 targetPoint)
    {
        //何かあるのかな？
    }

/// <summary>
/// 魔法を発射する処理
/// </summary>
/// <param name="castPoint"></param>
    public override void Cast(Transform castPoint)
    {
        SpendMana("回復魔法", ManaCost);

        Debug.Log(castPoint.position);
        // 魔法を発射
        currentSpell = Instantiate(spell.SpellPrefab, castPoint.position, Quaternion.identity);

        PlayerParameter playerParameter = player.GetComponent<PlayerParameter>();
        playerParameter.PlayerHeal(MagicDamage);

        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetHealSound();
    }

    /// <summary>
    /// 魔法を破壊する処理
    /// </summary>
    public void DestroySpell()
    {
        Destroy(currentSpell);
    }
}
