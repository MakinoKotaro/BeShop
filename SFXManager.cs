using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
/// <summary>
/// 効果音を再生する処理
/// </summary>
public class SFXManager : MonoBehaviour
{
    AudioSource audioSource; //AudioSourceを入れるもの
    private string clipName; //再生中の音の名前
    private float sfxLength; //効果音の長さ
    private bool isPlayingSFX = false; //効果音が再生中かどうか
    //======効果音たち===========================
    [SerializeField] private AudioClip startGameSound; //ゲーム開始の音
    [SerializeField] private AudioClip startStageSound; //ステージ開始の音
    [SerializeField] private AudioClip selectStageSound; //ステージ選択の音
    [SerializeField] private AudioClip backSound; //戻るボタンの音
    [SerializeField] private AudioClip shotSound; //攻撃発射の音
    [SerializeField] private AudioClip purchaseSound; //購入の音
    [SerializeField] private AudioClip selectBuyItemSound; //アイテム選択の音
    [SerializeField] private AudioClip changeItemAmountSound; //アイテム購入数変更の音
    [SerializeField] private AudioClip dodgeSound; //回避の音
    [SerializeField] private AudioClip swingAttackSound; //敵の振り下ろす攻撃の音
    [SerializeField] private AudioClip walkSound; //歩く音
    [SerializeField] private AudioClip healSound; //回復の音
    //============================================
    public float SfxLength { get => sfxLength; set => sfxLength = value; }
    public bool IsPlayingSFX { get => isPlayingSFX; set => isPlayingSFX = value; }

    private float stepInterval = 0.7f; //足音の間隔
    void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        audioSource = GetComponent<AudioSource>();
        if(SceneManager.GetActiveScene().name == "WalkToStageScene")
        {
            StartCoroutine(PlayWalkSound());
        }
    }
    /// <summary>
    /// 攻撃発射の音を設定
    /// </summary>
    public void SetShotSound()
    {
        audioSource.clip = shotSound;
        PlaySound();
    }

    /// <summary>
    /// ゲーム開始の音を設定
    /// </summary>
    public void SetGameStartSound()
    {
        audioSource.clip = startGameSound;
        PlaySound();
    }

    /// <summary>
    /// 購入音を設定
    /// </summary>
    public void SetPurchaseSound()
    {
        audioSource.clip = purchaseSound;
        PlaySound();
    }

    /// <summary>
    /// 購入アイテムを選択した音を設定
    /// </summary>
    public void SetSelectBuyItemSound()
    {
        audioSource.clip = selectBuyItemSound;
        PlaySound();
    }

    /// <summary>
    /// アイテムを買う数を変更する音を設定
    /// </summary>
    public void SetChangeItemAmountSound()
    {
        audioSource.clip = changeItemAmountSound;
        PlaySound();
    }

    /// <summary>
    /// 回避の音を設定
    /// </summary>
    public void SetDodgetSound()
    {
        audioSource.clip = dodgeSound;
        PlaySound();
    }

    /// <summary>
    /// 敵の振り下ろす攻撃の音を設定
    /// </summary>
    public void SetSwingAttackSound()
    {
        audioSource.clip = swingAttackSound;
        PlaySound();
    }

    /// <summary>
    /// 回復の音を設定
    /// </summary>
    public void SetHealSound()
    {
        audioSource.clip = healSound;
        PlaySound();
    }

    /// <summary>
    /// ステージセレクトで選択したときの音を設定
    /// </summary>
    public void OnClickSelectStage()
    {
        audioSource.clip = selectStageSound;
        PlaySound();
    }

    /// <summary>
    /// ステージ開始の音を設定
    /// </summary>
    public void OnClickStartStage()
    {
        audioSource.clip = startStageSound;
        PlaySound();
    }

    /// <summary>
    /// 戻るボタンの音を設定
    /// </summary>
    public void OnClickBackButton()
    {
        audioSource.clip = backSound;
        PlaySound();
    }
    /// <summary>
    /// 音を再生
    /// </summary>
    public void PlaySound()
    {
        isPlayingSFX = true;
        audioSource.Play();

        StartCoroutine(CalcSFXLength(audioSource.clip.length));
    }

    /// <summary>
    /// 効果音の長さを計算
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    IEnumerator CalcSFXLength(float length)
    {
        sfxLength = length;
        yield return new WaitForSeconds(length);
        isPlayingSFX = false;
    }

    /// <summary>
    /// ステージに移動する演出の際の足音を流す
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayWalkSound()
    {
        while (true)
        {
            audioSource.clip = walkSound;
            PlaySound();
            Debug.Log("a");
            yield return new WaitForSeconds(stepInterval);
        }
    }
}
