using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleManager�̃X�N���v�g
/// </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle; //�p�[�e�B�N�����擾

//�t�@�e�B���R�����g�G�@���̃X�N���v�g�͎g���Ă��Ȃ��̂ŁA�폜���Ă����Ȃ���ˁH
    void Update()
    {
        ////�X�y�[�X�L�[��������particle���Đ�
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("WADA");
        //    particle.Play();
        //}
        ////���V�t�g�L�[���N���b�N�����particle���~
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    Debug.Log("WWWW");
        //    particle.Stop();
        //}
    }
}
