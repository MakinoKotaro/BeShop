using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

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
