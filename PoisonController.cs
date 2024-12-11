using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 毒の挙動を管理する処理
/// </summary>
public class PoisonController : MonoBehaviour
{
    [SerializeField] private GameObject mushParent; // 親オブジェクト
    Mushroom mushroom; // Mushroomスクリプト

    private BoxCollider poisonCollider; // 毒のコライダー
    private GameObject player; // プレイヤー
    
    void Start()
    {
        poisonCollider = GetComponent<BoxCollider>();
        poisonCollider.enabled = false;
        mushroom = mushParent.GetComponent<Mushroom>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(mushroom.CanShotPoison == false)
        {
            poisonCollider.enabled = true;
        }
        else
        {
            poisonCollider.enabled = false;
        }
    }

    /// <summary>
    /// 毒がプレイヤーに接触しているときの処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mushroom = mushParent.GetComponent<Mushroom>();
            if (mushroom.CanShotPoison == true)
            {
                Debug.Log("canShotPoison");
                
            }
            else
            {
                PlayerParameter playerParameter = player.GetComponent<PlayerParameter>();
                playerParameter.PlayerTakePoison();
                playerParameter.TakePoison = true;
            }
        }
    }
}
