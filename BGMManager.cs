using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMを管理するスクリプト
/// </summary>
public class BGMManager : MonoBehaviour
{
    LoadScene loadScene;

    AudioSource audioSource;

    //======BGMクリップたち======
    [SerializeField] private AudioClip titleBgm;
    [SerializeField] private AudioClip HomeBgm;
    [SerializeField] private AudioClip ShopBgm;
    [SerializeField] private AudioClip StageBgm;
    [SerializeField] private AudioClip ClearBgm;
    [SerializeField] private AudioClip StageSelectBgm;
    //===========================
    private bool playingBgm = false;
    void Start()
    {
        loadScene = GetComponent<LoadScene>();

        audioSource = GetComponent<AudioSource>();

        playingBgm = false;
    }

    void Update()
    {
        //シーンによって再生するBGMを変更
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
            
        }
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    private void PlayBgm()
    {
        audioSource.Play();

        playingBgm = true;
    }
}
