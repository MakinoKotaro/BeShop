using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �񕜖��@�̃X�N���v�g
/// </summary>
public class HealMagic : MagicBase
{
    [SerializeField] private SO_Spell spell; //SO_Spell���擾

    private Vector3 targetPointPosition; // ���@�����n����ꏊ�̈ʒu���擾
    private List<GameObject> activeSpells = new List<GameObject>()�G // ���˂��ꂽ���@���������
    private GameObject currentSpell; // ���˂��ꂽ���@���������

    [SerializeField] float moveSpeed = 5f; // ���@�̑��x
    //[SerializeField] private float spellRange = 10f;

    [SerializeField] GameObject player; // Player���擾
    private PlayerController playerController; // PlayerController���擾

    GameObject castPoint; // ���@�𔭎˂���ꏊ

    Vector3 screenCenter; // ��ʂ̒��S
    Vector3 bulletDirection; // ���@�̕���

    [SerializeField] private GameObject particleManagerObj; // ParticleManager�̃I�u�W�F�N�g
    private GameObject sFXManagerObj; // SFXManager�̃I�u�W�F�N�g
    
    // Heal�̃R���X�g���N�^
    public HealMagic()
    {
        ManaCost = 0;
        MagicDamage = 2.0f;
    }

    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // player����PlayerController���擾

        sFXManagerObj = GameObject.FindWithTag("SFXManager");
    }
    public override void Behaviour(Vector3 targetPoint)
    {
        //��������̂��ȁH
    }

/// <summary>
/// ���@�𔭎˂��鏈��
/// </summary>
/// <param name="castPoint"></param>
    public override void Cast(Transform castPoint)
    {
        SpendMana("�񕜖��@", ManaCost);

        Debug.Log(castPoint.position);
        // ���@�𔭎�
        currentSpell = Instantiate(spell.SpellPrefab, castPoint.position, Quaternion.identity);

        PlayerParameter playerParameter = player.GetComponent<PlayerParameter>();
        playerParameter.PlayerHeal(MagicDamage);

        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetHealSound();
    }

    /// <summary>
    /// ���@��j�󂷂鏈��
    /// </summary>
    public void DestroySpell()
    {
        Destroy(currentSpell);
    }
}
