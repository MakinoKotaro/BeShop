using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMを管理するスクリプト
/// </summary>
public class BGMManager : MonoBehaviour
{
    LoadScene loadScene;　//シーン遷移を管理するスクリプト

    AudioSource audioSource; //オーディオソース

    //======BGMクリップたち======
    [SerializeField] private AudioClip titleBgm; //タイトルBGM
    [SerializeField] private AudioClip HomeBgm; //ホームBGM
    [SerializeField] private AudioClip ShopBgm; //ショップBGM
    [SerializeField] private AudioClip StageBgm; //ステージBGM
    [SerializeField] private AudioClip ClearBgm; //クリアBGM
    [SerializeField] private AudioClip StageSelectBgm; //ステージセレクトBGM
    //===========================
    private bool playingBgm = false; //BGMが再生中かどうか
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
            //何か追加するのかな？？
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
