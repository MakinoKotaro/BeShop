using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃A�j���[�V�������Ǘ�����X�N���v�g
/// </summary>
public class PlayerAnimationScript : MonoBehaviour
{
    //�A�j���[�^�[���擾
    Animator animator;

    [SerializeField] private GameObject sFXManageObj; //SFXManager�̃I�u�W�F�N�g

    //=============�e�A�j���[�V����=============
    [SerializeField] string idle_animation; //�ҋ@�A�j���[�V����
    [SerializeField] string normal_attack_animation; //�ʏ�U���A�j���[�V����
    [SerializeField] string area_masic_animation; //�͈͖��@�A�j���[�V����
    [SerializeField] string walk_animation; //�����A�j���[�V����
    [SerializeField] string dodge_animation; //������A�j���[�V����
    [SerializeField] string fall_animation; //�����A�j���[�V����
    [SerializeField] string dead_animation; //���S�A�j���[�V����
    //==========================================

    string nowAnimation = "";�@//���݂̃A�j���[�V�������擾
    private bool is_playing_animation = false; //���݃A�j���[�V�������Đ������ǂ���
    private bool stopping = true; //�v���C���[���~�܂��Ă��邩�ǂ��� 

    private float originalAnimationSpeed = 1f; //�ʏ�̃A�j���[�V�����X�s�[�h
    private float fastAnimationSpeed = 1.5f; //�����A�j���[�V�����X�s�[�h
    void Start()
    {
        animator = GetComponent<Animator>(); //�A�j���[�^�[�̃R���|�[�l���g���擾
    }

    
    void Update()
    {
        AnimatorStateInfo layer0_animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0); //���ݍĐ�����Ă��郌�C���[�O�̃A�j���[�V�����̏����擾
        AnimatorStateInfo layer1_animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1); //���ݍĐ�����Ă��郌�C���[�P�̃A�j���[�V�����̏����擾


        //�������͂��Ȃ��ꍇ�̃A�j���[�V��������
        if ((layer0_animatorStateInfo.normalizedTime >= 1f && layer0_animatorStateInfo.length > 0) || (layer1_animatorStateInfo.normalizedTime >= 1f && layer1_animatorStateInfo.length > 0) || stopping == true || is_playing_animation == true)
        {
            //Debug.Log("�A�j���[�V�����I��");
            is_playing_animation = false;

        }

        //Debug.Log("now animation speed :" + animator.speed);
        if(nowAnimation == "Dodge")
        {
            animator.speed = originalAnimationSpeed;
        }
        else
        {
            animator.speed = fastAnimationSpeed;
        }

        //Debug.Log("now animation = " + nowAnimation);
    }

    /// <summary>
    /// ��~��Ԃ̃A�j���[�V�������Đ�
    /// </summary>
    public void PlayIdleAnim()
    {
        nowAnimation = "Idle";
        stopping = true;
        animator.Play(idle_animation);
    }

    /// <summary>
    /// �����̃A�j���[�V�������Đ�
    /// </summary>
    public void PlayWalkAnim()
    {
        stopping = false;
        if (is_playing_animation == false)
        {
            nowAnimation = "Walk";
            animator.Play(walk_animation);
            is_playing_animation = true;
            
        }
    }

    /// <summary>
    /// ����A�j���[�V�������Đ�
    /// </summary>
    public void PlayRunAnim()
    {
        animator.Play(walk_animation);
    }

    /// <summary>
    /// �ʏ�U���̃A�j���[�V�������Đ�
    /// </summary>
    public void PlayNormalAttackAnim()
    {
        //Debug.Log("Attacking");

        nowAnimation = "NormalAttack";
        animator.Play(normal_attack_animation);
        is_playing_animation = true;
    }

    /// <summary>
    /// �͈͖��@�̃A�j���[�V�������Đ�
    /// </summary>
    public void PlayAreaMagicAnim()
    {
        nowAnimation = "AreaMagic";
        animator.Play(area_masic_animation);
        is_playing_animation = true;
    }

    /// <summary>
    /// ������A�j���[�V�������Đ�
    /// </summary>
    public void PlayDodgeAnim()
    {
        nowAnimation = "Dodge";
        animator.Play(dodge_animation, 2);
        is_playing_animation = true;

        SFXManager sFXManager = sFXManageObj.GetComponent<SFXManager>();
        sFXManager.SetDodgetSound();
    }

    /// <summary>
    /// �������̃A�j���[�V�������Đ�
    /// </summary>
    public void PlayFallAnim()
    {
        nowAnimation = "Fall";
        animator.Play(fall_animation);
        is_playing_animation = true;
    }

    /// <summary>
    /// ���񂾃A�j���[�V�����Đ�
    /// </summary>
    public void PlayDeadAnim()
    {
        nowAnimation = "Dead";
        animator.Play(dead_animation);
        is_playing_animation = true;
    }
}
