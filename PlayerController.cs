using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using Cinemachine;
using System.Runtime.InteropServices;

/// <summary>
/// プレイヤーの操作を管理するスクリプト
/// </summary>
public class PlayerController : MonoBehaviour  
{
    // このスクリプトで使う変数一覧

    private PlayerInput playerInput; // プレイヤーの入力を管理するための変数
    private CharacterController charaCon;       // キャラクターコンポーネント用の変数
    private Animator animCon;  //  アニメーションするための変数

    // 移動速度（SerializeField＝インスペクタで調整可能）
    [SerializeField]
    float moveSpeed = 5.0f;

    // プレイヤーの回転速度
    [SerializeField]
    float rotateSpeed = 1200.0f;   
    
    bool ClearFlag = false; //クリアフラグ

    //攻撃判定
    bool canAttack = true; //攻撃可能
    bool isAttacking = false; //攻撃中

    bool use_cant_move_spell = false; //移動不可の魔法を使ったかどうか

    //攻撃間隔, 経過時間
    float attackIntervalTime = 1f, elapsedTime;

    //地面にいるかどうか判定用
    private bool groundedPlayer = false;


    // キャラクター回避で加える力
    [SerializeField]
    private float dodgeForce = 12;

    //回避のクールタイム
    private bool canDodge = true;

    //プレイヤーの正面方向を取得
    [SerializeField] Vector3 playerForward;

    //重力
    private float gravityValue = -9.81f / 2;

    bool isFalling = false; //落下中かどうか

    float fallingTime = 0; //落下時間

    float fallTriggerTime = 0.5f; //落下アニメーションを再生するまでの時間

    private Vector3 playerVelocity; //プレイヤーの速度

    int score = 0; //スコア
    int nowScore = 0; //現在のスコア

    //地面判定
    [SerializeField]
    Transform groundCheck; //地面判定用のオブジェクト

    [SerializeField]
    float groundCheckRadius; //地面判定の半径

    [SerializeField]
    LayerMask[] groundLayers; //地面のレイヤー


    //何キルすればクリアか
    [SerializeField]
    int clearScore = 0;

    //キル数
    int kills = 0;

    public UnityEvent onClearCallback = new UnityEvent(); //クリア時のコールバック

    [SerializeField]
    private GameObject OnScreenControls; //スマホ用のコントロール

    private PlayerAnimationScript playerAnimationScript; //プレイヤーのアニメーションを管理するスクリプトを取得

    //MagicManagerを取得
    [SerializeField] private GameObject magicManager;

    //====魔法を発射する場所====     //複数種類詠唱アニメーションを追加予定のため、増えるかも
    [SerializeField] private Transform castPoint01;
    //==========================


    //====魔法を着地させる場所====     //複数種類詠唱アニメーションを追加予定のため、この変数も増えるかも
    [SerializeField] private Transform targetPoint01;

    public Transform TargetPoint01 { get => targetPoint01; set => targetPoint01 = value; }
    public bool CanDodge { get => canDodge; set => canDodge = value; }

    //==========================

    private float dodgeInterval = 0.5f; //回避のクールタイム
    private float normalAttackInterval = 0.3f; //通常攻撃のクールタイム

 
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); //PlayerInputを取得
        charaCon = GetComponent<CharacterController>(); // キャラクターコントローラーのコンポーネントを参照する
        animCon = GetComponent<Animator>(); // アニメーターのコンポーネントを参照する

        playerAnimationScript = GetComponent<PlayerAnimationScript>();
        //status = GetComponent<CharacterStatusScript>();

    }

    /// <summary>
    /// スコア更新
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private int ScoreUpdate(int s) 
    {

        score += s;

        return score;
    }

    void Start()
    {

        //この時点でスコアの処理を終わらせる必要がある

        //この後の処理をするには最新のスコアが必要
        //スコア表示のコード

        //使いたいから欲しい
        nowScore = ScoreUpdate(100);
      
       

#if UNITY_ANDROID

        if(OnScreenControls != null)
        {
            OnScreenControls.SetActive(true);
        }
#endif

#if UNITY_IOS

        if(OnScreenControls != null)
        {
            OnScreenControls.SetActive(true);
        }
#endif

#if UNITY_EDITOR

        if (OnScreenControls != null)
        {
            OnScreenControls.SetActive(true);
        }

#endif

#if UNITY_STANDALONE && !UNITY_EDITOR


        if (OnScreenControls != null)
        {
            OnScreenControls.SetActive(false);
        }

        // マウスカーソルを非表示にし、位置を固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    
#endif
    }
    // ■毎フレーム常に実行する処理
    void Update()
    {

        if (ClearFlag == true)
        {
            animCon.SetBool("Run", false);
            animCon.SetBool("Clear", true);

            charaCon.enabled = false;

            return;
        }

        //***移動処理***//
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        //Debug.Log("moveInput" + moveInput);

        //0 か１か -1にする
        moveInput = new Vector2(Mathf.Round(moveInput.x), Mathf.Round(moveInput.y));

        //Debug.Log("move x: " + moveInput.x);
        //Debug.Log("move x: " + moveInput.y);

        //移動していなく、回避可能な状態で、落下中でないなら
        if (moveInput == Vector2.zero && canDodge == true && isFalling == false)
        {
            //Debug.Log("no input");
            //animCon.SetBool("Run", false);  //  Runモーションしない

            playerAnimationScript.PlayIdleAnim();
        }
        //移動してるか、回避中か、落下中なら
        else
        {
            var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;  //  カメラが追従するための動作

            //Vector3 direction = cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");  //  テンキーや3Dスティックの入力（GetAxis）があるとdirectionに値を返す
            Vector3 direction = cameraForward * moveInput.y + Camera.main.transform.right * moveInput.x;

            //animCon.SetBool("Run", true);  //  Runモーションする

            //移動のみしている場合
            if (canDodge == true && isFalling == false)
            {
                playerAnimationScript.PlayWalkAnim(); //歩きアニメーションを再生
            }

            
            if (canDodge == true && use_cant_move_spell == false)
            {
                //向きを変える動作の処理を実行する
                ChangeDirection(direction);

                //移動する動作の処理を実行する
                charaCon.Move(direction * Time.deltaTime * moveSpeed);   // プレイヤーの移動距離は時間×移動スピードの値
            }
        }

        //足元に地面があるかチェックする
        foreach (LayerMask groundLayer in groundLayers)
        {
            groundedPlayer = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        //Debug.Log("Grounded " + groundedPlayer);
        
        //避けるアクション（スペースキー）が押されていて、回避可能な状態のとき、
        if (playerInput.actions["Dodge"].triggered && canDodge == true)
        {
            StartCoroutine(Dodge()); //避けるコルーチンを開始
        }

        if (playerInput.actions["Cancel"].triggered)
        {

            Debug.Log("キャンセル");
        }

        //プレイヤーが地面にいなくて、まだ落下判定が出ていない場合、落下判定にする
        if (groundedPlayer == false && isFalling == false)
        {
            isFalling = true;
        }

        //プレイヤーが地面にいて、落下判定が出ている場合、それを消す
        if (groundedPlayer == true && isFalling == true)
        {
            isFalling = false;
        }

        //落下時間が再生のに必要な時間を超えた場合
        if (fallingTime > fallTriggerTime)
        {
            Fall(); //落下アニメーションを再生
        }

        //Debug.Log(isFalling);
        //Debug.Log(groundedPlayer);

        //重力
        playerVelocity.y += gravityValue * Time.deltaTime;

        //  移動する動作の処理を実行する
        charaCon.Move(playerVelocity * Time.deltaTime);   // プレイヤーの移動距離は時間×移動スピードの値


        // ▼▼▼アクション処理▼▼▼
        //  ボタンを押したらアクションする
        if (playerInput.actions["Attack"].triggered && canAttack && !isAttacking)
        {

            StartCoroutine(Attack()); //攻撃の処理開始

            canAttack = false;

        }

        //右クリックした場合
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            StartCoroutine(Attack02()); //右クリック用の攻撃を開始
        }

        if (!canAttack)
        {

            elapsedTime += Time.deltaTime;

            if (elapsedTime > attackIntervalTime)
            {

                elapsedTime = 0;
                canAttack = true;

            }
        }


        playerForward = Camera.main.transform.forward;
        playerForward.y = 0;
        //Debug.Log(playerForward.ToString());
        Quaternion playerRotation = Quaternion.LookRotation(playerForward);
        gameObject.transform.rotation = playerRotation;
    }

    //選択されたら地面チェック用のオブジェクトを描く
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

/// <summary>
/// キル数をカウントする
/// </summary>
    public void CountKills()
    {
        kills++;

        if (kills >= clearScore)
        {
            onClearCallback.Invoke();

        }
    }

    /// <summary>
    /// 左クリックの攻撃コルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator Attack()
    {

        playerAnimationScript.PlayNormalAttackAnim(); //左クリックの攻撃アニメーションを再生
        isAttacking = true; //攻撃中にする

        yield return new WaitForSeconds(0.15f); //0.15秒待って

        NormalAttack normalAttack = magicManager.GetComponent<NormalAttack>(); //MagicManager内のNormalAttackを取得し、

        //castPoint01の位置からtargetPoint01の位置に魔法発射
        normalAttack.Cast(castPoint01); 
        normalAttack.Behaviour(targetPoint01.position); 

        yield return new WaitForSeconds(normalAttackInterval); //0.30秒待って

        isAttacking = false; //攻撃可能にする

    }

    /// <summary>
    /// 右クリックの攻撃コルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator Attack02()
    {
        if (use_cant_move_spell == false)
        {
            playerAnimationScript.PlayAreaMagicAnim(); //右クリックの攻撃アニメーションを再生
            use_cant_move_spell = true; //移動不可の攻撃をした
            isAttacking = true; //攻撃不可にする
            yield return new WaitForSeconds(0.8f); //0.8秒待って >> いいけど、変数化した方がいいかも
            HealMagic healMagic = magicManager.GetComponent<HealMagic>();

            healMagic.Cast(gameObject.transform);
            yield return new WaitForSeconds(1.0f); //1.0秒待って　>> いいけど、変数化した方がいいかも

            healMagic.DestroySpell();
        }
        use_cant_move_spell = false; //移動不可の攻撃をしていない判定にする
        isAttacking = false; //攻撃可能にする
    }

    //IsAttackingを探す
    public bool GetIsAttacking()
    {
        return isAttacking;
    }
    //ISAttackingを外から操作できるようにする
    public void SetIsAttacking(bool b)
    {

        isAttacking = b;
    }

    /// <summary>
    /// 回避コルーチン開始
    /// </summary>
    /// <returns></returns>
    IEnumerator Dodge()
    {
        canDodge = false; //回避不可にする

        //プレイヤーが向いている方向に回避する処理
        playerForward.y = 0;
        playerForward.Normalize();
        playerVelocity = playerForward * dodgeForce;
        playerAnimationScript.PlayDodgeAnim();

        yield return new WaitForSeconds(dodgeInterval); //0.5秒待って

        playerVelocity = playerForward / dodgeForce; //回避の移動をやめる
        canDodge = true; //回避可能にする
    }

    /// <summary>
    /// 落下アニメーションの処理
    /// </summary>
    public void Fall()
    {
        playerAnimationScript.PlayFallAnim(); //落下アニメーション再生
        //isFalling = true;
    }

    public void ClearFlags(bool c)
    {
        ClearFlag = c;
    }

    // ■向きを変える動作の処理
    void ChangeDirection(Vector3 direction)
    {
        Quaternion q = Quaternion.LookRotation(direction);          // 向きたい方角をQuaternion型に直す
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);   // 向きを q に向けてじわ～っと変化させる.
    }

    //話が終わったとき
    public void OnFinishedTalking()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }

    private void FixedUpdate()
    {
        //落下中に落下時間を計算する
        if (isFalling == true)
        {
            fallingTime += Time.deltaTime;
        }
        else
        {
            fallingTime = 0;
        }
    }

    //ファティンコメント：必要ない部分は消しましょう。参考に残したいコードがある場合は違うファイルに移動してください。
    //後、このままだとリモコンに対応していないゲームになるので、NewInputSystemのリモコンに対応するためのコードに変更してください。
}
