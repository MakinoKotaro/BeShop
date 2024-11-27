using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] private string victory_animation;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(victory_animation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
