using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistAnimController : MonoBehaviour
{
    public Animator animator;

    public enum AnimState
    {
        Idle,
        Walk,
        Chair
    }

    public AnimState StateSelector;

    void Update()
    {
        switch (StateSelector)
        {
            case AnimState.Idle:
                animator.SetBool("isIdle", true);
                animator.SetBool("isWalk", false);
                break;
            case AnimState.Walk:
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalk", true);
                break;
            case AnimState.Chair:
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalk", false);
                break;
        }
    }
}