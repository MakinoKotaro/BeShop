using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�F�L�m�R�̃X�N���v�g
/// </summary>
public class Mushroom : Enemy, IE_SpecialMove
{
    [SerializeField] private GameObject enemyManagerObj; //�G�̊Ǘ��X�N���v�g�̃I�u�W�F�N�g
    private EnemyManager _enemyManager; //�G�̊Ǘ��X�N���v�g


    [SerializeField] private float mushroomHealth; //�L�m�R�̗̑�
    [SerializeField] private int mushroomAttackPower; //�L�m�R�̍U����

    [SerializeField] private float mushroomMoveSpeed = 5f; //�L�m�R�̈ړ����x

    Animator animator;
    //=============�e�A�j���[�V����=============
    [SerializeField] string idle_animation;
    [SerializeField] string attack_animation;
    [SerializeField] string walk_animation;
    [SerializeField] string dead_animation;
    //==========================================

    private GameObject player; //�v���C���[�̃I�u�W�F�N�g
    private bool playerIsHere = false; //�v���C���[�����邩�ǂ���

    private float destroyDelay = 1.8f; //�j�󂳂��܂ł̎���
    private float attackInterval = 5.0f; //�U���Ԋu
    private float popPoisonDelay = 0.5f; //�ł̃G�t�F�N�g���o���܂ł̎���
    private float distanceToPlayer = 3.0f; //�v���C���[�Ƃ̋����̂������l

    //����s���p�̕ϐ�����
    [SerializeField] private ParticleSystem poisonParticle; //�ł̃G�t�F�N�g
    PoisonController poisonController; //�ł̃R���g���[���[
    private bool canShootPoison = true; //�ł��o�����Ƃ��\���ǂ���

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
    /// �ړ��̏���
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
    /// �U���̏���
    /// </summary>
    public override void EnemyAttack()
    {
        StartCoroutine(Attack());
    }

    /// <summary>
    /// �U�����~�߂鏈��
    /// </summary>
    public override void EnemyStopAttack()
    {
        StopCoroutine(Attack());
    }

    /// <summary>
    /// �G���U�����󂯂鏈��
    /// </summary>
    /// <param name="damageAmount"></param>
    public override void EnemyTakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("now HP �F " + health);
        if (health <= 0)
        {
            EnemyDead();
        }
    }


    /// <summary>
    /// �G�����ʏ���
    /// </summary>
    public override void EnemyDead()
    {
        animator.Play(dead_animation);
        canMove = false;

        Invoke("DestroyObj", destroyDelay);
    }

    /// <summary>
    /// �G���폜���鏈��
    /// </summary>
    void DestroyObj()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// �G�̓��ʂȓ����i�ł�f���j����
    /// </summary>
    public void EnemySpecialMove()
    {
        canShootPoison = false;
        poisonParticle.Play();
        StartCoroutine(SpecialAttack());
    }

    /// <summary>
    /// �ړ��R���[�`��
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    IEnumerator Move(Vector3 playerPosition)
    {
        while (playerIsHere == true)
        {
            animator.Play(walk_animation);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, mushroomMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerPosition) < distanceToPlayer) // �����̂������l
            {
                break; // �ڕW�ʒu�ɓ��B�����烋�[�v�𔲂���
            }

            yield return null;
        }

    }

    /// <summary>
    /// �U���R���[�`��
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
    /// ���ʂȍs���i�œf���j�R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(1.0f);
        canShootPoison = true;
    }
}
