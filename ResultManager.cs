using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルト画面のアニメーションを管理するスクリプト
/// </summary>
public class ResultManager : MonoBehaviour
{
    Animator animator; //アニメーター
    [SerializeField] private string victory_animation; //勝利アニメーションの名前
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(victory_animation);
    }

}
