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
    private static float playerHitPoint = 10; // プレイヤーのHP
    private static float playerPower = 5; // プレイヤーの攻撃力
    private static float playerSpeed = 3; // プレイヤーの移動速度
    private float maxHealth; // プレイヤーの最大HP
    private bool takePoison = false; // 毒を受けたかどうか
    //==========================

    private float warningHitPoint; // HPがこの値を下回ると警告UIを表示する


    //======ステータス プロパティ======
    public float PlayerHitPoint { get => playerHitPoint; set => playerHitPoint = value; }
    public float PlayerPower { get => playerPower; set => playerPower = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool TakePoison { get => takePoison; set => takePoison = value; }
    public bool NowTakingPoison { get => nowTakingPoison; set => nowTakingPoison = value; }

    //=======================================

    private bool nowTakingPoison = false; //今毒を受けているかどうか

    [SerializeField] private GameObject hpBarManagerObj;  // HPバーのオブジェクト
    GameObject warningUIProcessingObj; // 警告UIのオブジェクト
    WarningUIProcessing warningUIProcessing; // 警告UIのスクリプト
    private bool isShowingWarningUI = false; // 警告UIを表示しているかどうか

    PlayerController playerController; // プレイヤーのコントローラー
    private void Start()
    {
        maxHealth = playerHitPoint;
        warningHitPoint = maxHealth / 3; //マジックナンバー発見！変数化してください。
    }

    private void Update()
    {
        //これはデバッグ用かな？ならばUnity_EDITORを使うべきです。

        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            StopCoroutine(TakingPoison());
        }

        
        if (playerHitPoint <= warningHitPoint && isShowingWarningUI == false)
        {
            warningUIProcessing = GetComponent<WarningUIProcessing>();
            warningUIProcessing.canShowWarningUI = true;

            isShowingWarningUI = true;
        }
        else
        {
            isShowingWarningUI = false;

            warningUIProcessing = GetComponent<WarningUIProcessing>();
            warningUIProcessing.canShowWarningUI = false;
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
        
        for (int i = 0; i < 10; i++) //マジックナンバー発見！変数化してください。
        {
            PlayerTakeDamage(0.2f);　//マジックナンバー発見！変数化してください。

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
        //処理追加予定　（特殊な効果を発動させる）
    }
}
