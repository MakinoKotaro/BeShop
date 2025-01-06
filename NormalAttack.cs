using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// �ʏ�U���̃X�N���v�g
/// </summary>
public class NormalAttack : MagicBase
{
    [SerializeField] private SO_Spell spell; //SO_Spell���擾

    private Vector3 targetPointPosition; // ���@�����n����ꏊ�̈ʒu���擾
    private List<GameObject> activeSpells = new List<GameObject>();
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

    private float spellLength = 10.0f; // ���@�̒���
    private float moveLength = 0.5f; // ���@�̈ړ�����
    private float destroyDelay = 0.7f; // ���@�̏��ł܂ł̎���
    
    // NormalAttack�̃R���X�g���N�^
    public NormalAttack()
    {
        ManaCost = 5;  //����萔�ɂ��邩�ϐ��ɂ��邩�ɂ��Ă�������
        MagicDamage = 5; //����萔�ɂ��邩�ϐ��ɂ��邩�ɂ��Ă�������
    }

    void Start()
    {
        playerController = player.GetComponent<PlayerController>(); // player����PlayerController���擾

        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0).normalized;
        //�}�W�b�N�i���o�[�����I�R�����g�ǉ��Ȃǂ��Ă�������

        sFXManagerObj = GameObject.FindWithTag("SFXManager");
    }

/// <summary>
/// ���@�̋���
/// </summary>
/// <param name="targetPoint"></param>
    public override void Behaviour(Vector3 targetPoint)
    {

        castPoint = GameObject.FindWithTag("CastPoint");
        //DOTween�Ŗ��@�̋���������
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 targetPosition = cameraPosition + cameraForward * spellLength;

        bulletDirection = targetPosition - castPoint.transform.position;

        currentSpell.transform.DOLocalMove(bulletDirection, moveLength).SetRelative(true).SetEase(Ease.Linear);
        Invoke("DestroySpell", destroyDelay);
    }

/// <summary>
/// ���@�̔���
/// </summary>
/// <param name="castPoint"></param>
    public override void Cast(Transform castPoint)
    {
        SpendMana("�ʏ�U��", ManaCost);

        // ���ˎ���targetPoint�̈ʒu���擾
        targetPointPosition = playerController.TargetPoint01.position; // Player��targetPoint���擾

        // ���@�𔭎�
        currentSpell = Instantiate(spell.SpellPrefab, castPoint.position, Quaternion.identity);

        SFXManager sFXManager = sFXManagerObj.GetComponent<SFXManager>();
        sFXManager.SetShotSound();
    }

/// <summary>
/// ���@����������
/// </summary>
    void DestroySpell()
    {
        Destroy(currentSpell);
    }



    private void OnCollisionEnter(Collision collision)
    {

        //string collidedObjectName = collision.gameObject.name;

        //// �Փ˂����I�u�W�F�N�g�̖��O��\��
        //Debug.Log("�Փ˂����I�u�W�F�N�g�̖��O: " + collidedObjectName);


        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy");

            IE_TakeDamage e_TakeDamage = collision.gameObject.GetComponent<IE_TakeDamage>();

            e_TakeDamage.EnemyTakeDamage(MagicDamage);

            ContactPoint contact = collision.contacts[0];

            // �Փ˓_�̈ʒu�ix, y, z�j
            Vector3 collisionPoint = contact.point;

            DestroySpell();
        }
    }
}

