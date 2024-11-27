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
    AudioSource audioSource;
    private string clipName;
    private float sfxLength;
    private bool isPlayingSFX = false;
    //======効果音たち===========================
    [SerializeField] private AudioClip startGameSound;
    [SerializeField] private AudioClip startStageSound;
    [SerializeField] private AudioClip selectStageSound;
    [SerializeField] private AudioClip backSound;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip purchaseSound;
    [SerializeField] private AudioClip selectBuyItemSound;
    [SerializeField] private AudioClip changeItemAmountSound;
    [SerializeField] private AudioClip dodgeSound;
    [SerializeField] private AudioClip swingAttackSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip healSound;
    //============================================
    public float SfxLength { get => sfxLength; set => sfxLength = value; }
    public bool IsPlayingSFX { get => isPlayingSFX; set => isPlayingSFX = value; }

    private float stepInterval = 0.7f;
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
