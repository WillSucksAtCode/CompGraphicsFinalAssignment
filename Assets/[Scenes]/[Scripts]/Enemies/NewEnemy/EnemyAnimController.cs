using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    public Animator animator;

    public enum AnimState
    {
        Wander,
        Chase,
        Attack,
        Die
    }

    public AnimState StateSelector;

    void Update()
    {
        switch (StateSelector)
        {
            case AnimState.Wander:
                animator.SetBool("isWander", true);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttack", false);
                break;
            case AnimState.Chase:
                animator.SetBool("isWander", false);
                animator.SetBool("isChasing", true);
                animator.SetBool("isAttack", false);
                break;
            case AnimState.Attack:
                animator.SetBool("isWander", false);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttack", true);
                break;
            case AnimState.Die:
                animator.SetBool("isDead", true);
                animator.SetBool("isWander", false);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttack", false);
                break;
        }
    }
}