using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[��������X�N���v�g
/// </summary>
public class EnemySearchPlayer : MonoBehaviour
{
    [SerializeField] private float radius; //�{�����a
    [SerializeField] private Enemy enemy; //�G�̃X�N���v�g

    private float loopTime = 0.01f; //���[�v�̃N�[���^�C��
    [SerializeField] private float attackLoopTime = 3f; //�U���̃N�[���^�C��

    private bool canAttack = true; //�U���\���ǂ���

    [SerializeField] private float attackDistance; //�U������
    [SerializeField] private float rayDistance; //���C�̋���
    private string targetTag = "Player"; //�^�[�Q�b�g�̃^�O

    private bool playerInFront = false; //�v���C���[�����ʂɂ��邩�ǂ���


    Vector3 playerPosition = Vector3.zero; //�v���C���[�̈ʒu

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
        // �f�o�b�O�p�Ƀ��C����������
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
    }

    /// <summary>
    /// �v���C���[�{���R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator SearchPlayer()
    {
        while (true) // �������[�v�ɂȂ肩�˂Ȃ��̂ŁA�����ƃt���O�Ƃ��p�ӂ�����������
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            bool playerInRange = false;

            // �擾�����R���C�_�[�����[�v
            foreach (Collider collider in hitColliders)
            {
                // �^�O��"Player"�̏ꍇ
                if (collider.CompareTag("Player"))
                {
                    playerInRange = true;
                    playerPosition = collider.transform.position; // �v���C���[�̈ʒu���擾

                    Vector3 directionToPlayer = (collider.transform.position - transform.position).normalized;
                    // Y�������O���Č�����ݒ�
                    directionToPlayer.y = 0; // Y�������[���ɂ���
                    if (directionToPlayer != Vector3.zero) // �[���x�N�^�[���`�F�b�N
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                        transform.rotation = targetRotation;
                    }
                }
            }

            // �v���C���[���͈͓��ɂ��邩�ǂ����ŏ�����ς���
            if (playerInRange)
            {
                //Debug�p�̃R�[�h�Ȃ��Unity_EDITOR���g���ׂ��ł��B
                #if UNITY_EDITOR
                //Debug.Log("Player is here�F" + playerPosition);
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
    /// �G�̍U���̏������Ăяo�����𔻒肷�鏈��
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackPlayer()
    {
        

        // �I�u�W�F�N�g�̐��ʕ����Ƀ��C���΂�
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            // ���������I�u�W�F�N�g�̃^�O���`�F�b�N
            if (hit.collider.CompareTag(targetTag) && enemy != null)
            { 
                enemy.EnemyAttack();
                // ���C���w�肵���^�O�̃I�u�W�F�N�g�ɓ��������Ƃ��̏���
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
    /// �U��������o�����t���[�����A�ڂ̑O�Ƀv���C���[�����邩�ǂ����𔻒�
    /// </summary>
    public void ChackPlayerFrame()
    {
        // �I�u�W�F�N�g�̐��ʕ����Ƀ��C���΂�
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            // ���������I�u�W�F�N�g�̃^�O���`�F�b�N
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
