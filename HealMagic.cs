using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealMagic : MagicBase
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

    }

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

    public void DestroySpell()
    {
        Destroy(currentSpell);
    }
}
