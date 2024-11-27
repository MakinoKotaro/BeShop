using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのパラメータを管理するスクリプト
/// </summary>
public class PlayerParameter : MonoBehaviour
{
    //======ステータス変数======
    private static float playerHitPoint = 10;
    private static float playerPower = 5;
    private static float playerSpeed = 3;
    private float maxHealth;
    private bool takePoison = false;
    //==========================

    private float warnigHitPoint;


    //======ステータス プロパティ======
    public float PlayerHitPoint { get => playerHitPoint; set => playerHitPoint = value; }
    public float PlayerPower { get => playerPower; set => playerPower = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool TakePoison { get => takePoison; set => takePoison = value; }
    public bool NowTakingPoison { get => nowTakingPoison; set => nowTakingPoison = value; }

    //=======================================

    private bool nowTakingPoison = false;

    [SerializeField] private GameObject hpBarManagerObj;
    GameObject warnigUiProcessingObj;
    WarnigUiProcessing warnigUiProcessing;
    private bool isShowingWarningUi = false;

    PlayerController playerController;
    private void Start()
    {
        maxHealth = playerHitPoint;
        warnigHitPoint = maxHealth / 3;
    }

    private void Update()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            StopCoroutine(TakingPoison());
        }

        

        if (playerHitPoint <= warnigHitPoint && isShowingWarningUi == false)
        {
            warnigUiProcessing = GetComponent<WarnigUiProcessing>();
            warnigUiProcessing.CanShowWarningUi = true;

            isShowingWarningUi = true;
        }
        else
        {
            isShowingWarningUi = false;

            warnigUiProcessing = GetComponent<WarnigUiProcessing>();
            warnigUiProcessing.CanShowWarningUi = false;
        }
    }

    /// <summary>
    /// プレイヤーがダメージを受ける処理
    /// </summary>
    /// <param name="takeDamage"></param>
    public void PlayerTakeDamage(float takeDamage)
    {
        playerHitPoint -= takeDamage;
        Debug.Log($"現在のHP：{playerHitPoint / maxHealth}");
        float hp = playerHitPoint / maxHealth;

        HpBarManager hpBarManager = hpBarManagerObj.GetComponent<HpBarManager>();

        if (playerHitPoint <= 0)
        {
            PlayerAnimationScript playerAnimationScript = GetComponent<PlayerAnimationScript>();
            playerAnimationScript.PlayDeadAnim();
        }


        if (hpBarManager != null)
        {
            hpBarManager.ShowCurrentHp(hp);
        }
        else
        {
            Debug.LogError("HpBarManagerが見つかりません。hpBarManagerObjに正しいオブジェクトを設定してください。");
        }
        //hpBarManager.ShowCurrentHp( hp );
    }

    /// <summary>
    /// プレイヤーが回復する処理
    /// </summary>
    /// <param name="healAmount"></param>
    public void PlayerHeal(float healAmount)
    {
        playerHitPoint += healAmount;  // 回復量を追加

        // 最大HPを超えないようにする
        if (playerHitPoint > maxHealth)
        {
            playerHitPoint = maxHealth;
        }

        Debug.Log($"現在のHP：{playerHitPoint / maxHealth}");

        // HPバーの更新
        float hp = playerHitPoint / maxHealth;
        HpBarManager hpBarManager = hpBarManagerObj.GetComponent<HpBarManager>();

        if (hpBarManager != null)
        {
            hpBarManager.ShowCurrentHp(hp);
        }
        else
        {
            Debug.LogError("HpBarManagerが見つかりません。hpBarManagerObjに正しいオブジェクトを設定してください。");
        }
    }

    /// <summary>
    /// プレイヤーが毒を受けた時の処理
    /// </summary>
    public void PlayerTakePoison()
    {
        if (nowTakingPoison == false)
        {
            StartCoroutine(TakingPoison());
            nowTakingPoison = true;
        }
    }

    /// <summary>
    /// 毒ダメージのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator TakingPoison()
    {
        
        for (int i = 0; i < 10; i++)
        {
            PlayerTakeDamage(0.2f);
            yield return new WaitForSeconds(1);  // 1秒ごとに待機してループ
        }
        takePoison = false;
        nowTakingPoison = false;
    }

    

    /// <summary>
    /// 購入したアイテムが+計算のものだったときの処理
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="itemCount"></param>
    public void AddParameter(string type, float amount, int itemCount)
    {
        switch (type)
        {
            case "damage":
                playerPower += amount * itemCount;
                Debug.Log("now playerPower：" + playerPower);
                break;
            case "hp":
                playerHitPoint += amount * itemCount;
                Debug.Log("now HitPoint：" + playerHitPoint);
                break;
            case "speed":
                playerSpeed += amount * itemCount;
                Debug.Log("now playerSpeed：" + playerSpeed);
                break;
        }
    }

    /// <summary>
    /// 購入したアイテムが-計算のものだったときの処理
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <param name="itemCount"></param>
    public void MulParameter(string type, float amount, int itemCount)
    {
        switch (type)
        {
            case "damage":
                playerPower *= amount * itemCount;
                Debug.Log("now playerPower：" + playerPower);
                break;
            case "hp":
                playerHitPoint *= amount * itemCount;
                Debug.Log("now HitPoint：" + playerHitPoint);
                break;
            case "speed":
                playerSpeed *= amount * itemCount;
                Debug.Log("now playerSpeed：" + playerSpeed);
                break;
        }
    }

    /// <summary>
    /// 購入したアイテムに特殊な効果がある場合の処理
    /// </summary>
    /// <param name="itemName"></param>
    public void UnlockSpecialMove(string itemName)
    { 
        
    }
}
