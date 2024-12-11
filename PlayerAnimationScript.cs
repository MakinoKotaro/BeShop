using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションを管理するスクリプト
/// </summary>
public class PlayerAnimationScript : MonoBehaviour
{
    //アニメーターを取得
    Animator animator;

    [SerializeField] private GameObject sFXManageObj; //SFXManagerのオブジェクト

    //=============各アニメーション=============
    [SerializeField] string idle_animation; //待機アニメーション
    [SerializeField] string normal_attack_animation; //通常攻撃アニメーション
    [SerializeField] string area_masic_animation; //範囲魔法アニメーション
    [SerializeField] string walk_animation; //歩くアニメーション
    [SerializeField] string dodge_animation; //避けるアニメーション
    [SerializeField] string fall_animation; //落下アニメーション
    [SerializeField] string dead_animation; //死亡アニメーション
    //==========================================

    string nowAnimation = "";　//現在のアニメーションを取得
    private bool is_playing_animation = false; //現在アニメーションを再生中かどうか
    private bool stopping = true; //プレイヤーが止まっているかどうか 

    private float originalAnimationSpeed = 1f; //通常のアニメーションスピード
    private float fastAnimationSpeed = 1.5f; //速いアニメーションスピード
    void Start()
    {
        animator = GetComponent<Animator>(); //アニメーターのコンポーネントを取得
    }

    
    void Update()
    {
        AnimatorStateInfo layer0_animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0); //現在再生されているレイヤー０のアニメーションの情報を取得
        AnimatorStateInfo layer1_animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1); //現在再生されているレイヤー１のアニメーションの情報を取得


        //何も入力がない場合のアニメーション処理
        if ((layer0_animatorStateInfo.normalizedTime >= 1f && layer0_animatorStateInfo.length > 0) || (layer1_animatorStateInfo.normalizedTime >= 1f && layer1_animatorStateInfo.length > 0) || stopping == true || is_playing_animation == true)
        {
            //Debug.Log("アニメーション終了");
            is_playing_animation = false;

        }

        //Debug.Log("now animation speed :" + animator.speed);
        if(nowAnimation == "Dodge")
        {
            animator.speed = originalAnimationSpeed;
        }
        else
        {
            animator.speed = fastAnimationSpeed;
        }

        //Debug.Log("now animation = " + nowAnimation);
    }

    /// <summary>
    /// 停止状態のアニメーションを再生
    /// </summary>
    public void PlayIdleAnim()
    {
        nowAnimation = "Idle";
        stopping = true;
        animator.Play(idle_animation);
    }

    /// <summary>
    /// 歩きのアニメーションを再生
    /// </summary>
    public void PlayWalkAnim()
    {
        stopping = false;
        if (is_playing_animation == false)
        {
            nowAnimation = "Walk";
            animator.Play(walk_animation);
            is_playing_animation = true;
            
        }
    }

    /// <summary>
    /// 走るアニメーションを再生
    /// </summary>
    public void PlayRunAnim()
    {
        animator.Play(walk_animation);
    }

    /// <summary>
    /// 通常攻撃のアニメーションを再生
    /// </summary>
    public void PlayNormalAttackAnim()
    {
        //Debug.Log("Attacking");

        nowAnimation = "NormalAttack";
        animator.Play(normal_attack_animation);
        is_playing_animation = true;
    }

    /// <summary>
    /// 範囲魔法のアニメーションを再生
    /// </summary>
    public void PlayAreaMagicAnim()
    {
        nowAnimation = "AreaMagic";
        animator.Play(area_masic_animation);
        is_playing_animation = true;
    }

    /// <summary>
    /// 避けるアニメーションを再生
    /// </summary>
    public void PlayDodgeAnim()
    {
        nowAnimation = "Dodge";
        animator.Play(dodge_animation, 2);
        is_playing_animation = true;

        SFXManager sFXManager = sFXManageObj.GetComponent<SFXManager>();
        sFXManager.SetDodgetSound();
    }

    /// <summary>
    /// 落下中のアニメーションを再生
    /// </summary>
    public void PlayFallAnim()
    {
        nowAnimation = "Fall";
        animator.Play(fall_animation);
        is_playing_animation = true;
    }

    /// <summary>
    /// 死んだアニメーション再生
    /// </summary>
    public void PlayDeadAnim()
    {
        nowAnimation = "Dead";
        animator.Play(dead_animation);
        is_playing_animation = true;
    }
}
