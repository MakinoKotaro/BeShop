using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM���Ǘ�����X�N���v�g
/// </summary>
public class BGMManager : MonoBehaviour
{
    LoadScene loadScene;�@//�V�[���J�ڂ��Ǘ�����X�N���v�g

    AudioSource audioSource; //�I�[�f�B�I�\�[�X

    //======BGM�N���b�v����======
    [SerializeField] private AudioClip titleBgm; //�^�C�g��BGM
    [SerializeField] private AudioClip HomeBgm; //�z�[��BGM
    [SerializeField] private AudioClip ShopBgm; //�V���b�vBGM
    [SerializeField] private AudioClip StageBgm; //�X�e�[�WBGM
    [SerializeField] private AudioClip ClearBgm; //�N���ABGM
    [SerializeField] private AudioClip StageSelectBgm; //�X�e�[�W�Z���N�gBGM
    //===========================
    private bool playingBgm = false; //BGM���Đ������ǂ���
    void Start()
    {
        loadScene = GetComponent<LoadScene>();

        audioSource = GetComponent<AudioSource>();

        playingBgm = false;
    }

    void Update()
    {
        //�V�[���ɂ���čĐ�����BGM��ύX
        if (playingBgm == false)
        {
            switch (loadScene.CurrentScene)
            {
                case "TitleScene":
                    audioSource.clip = titleBgm;
                    PlayBgm();
                    break;
                case "HomeScene":
                    audioSource.clip = HomeBgm;
                    PlayBgm();
                    break;
                case "ShopScene":
                    audioSource.clip = ShopBgm;
                    PlayBgm();
                    break;
                case "Stage":
                    audioSource.clip = StageBgm;
                    PlayBgm();
                    break;
                case "ResultScene":
                    audioSource.clip = ClearBgm;
                    PlayBgm();
                    break;
                case "StageSelectScene":
                    audioSource.clip = StageSelectBgm;
                    PlayBgm();
                    break;
            }
        }

        if(playingBgm == false)
        {
            //�����ǉ�����̂��ȁH�H
        }
    }

    /// <summary>
    /// BGM�Đ�
    /// </summary>
    private void PlayBgm()
    {
        audioSource.Play();

        playingBgm = true;
    }
}
