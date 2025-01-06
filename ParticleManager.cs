using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ParticleManagerのスクリプト
/// </summary>
public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle; //パーティクルを取得

//ファティンコメント；　このスクリプトは使われていないので、削除しても問題ないよね？
    void Update()
    {
        ////スペースキーを押すとparticleを再生
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("WADA");
        //    particle.Play();
        //}
        ////左シフトキーをクリックするとparticleを停止
        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    Debug.Log("WWWW");
        //    particle.Stop();
        //}
    }
}
