using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���U���g��ʂ̃A�j���[�V�������Ǘ�����X�N���v�g
/// </summary>
public class ResultManager : MonoBehaviour
{
    Animator animator; //�A�j���[�^�[
    [SerializeField] private string victory_animation; //�����A�j���[�V�����̖��O
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(victory_animation);
    }

}
