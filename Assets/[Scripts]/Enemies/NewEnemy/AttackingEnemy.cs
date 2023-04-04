using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : MonoBehaviour
{
    bool isAttacking = false;
    public GameObject anim;

    private void Update()
    {
        if (!isAttacking)
            StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        float waitTime = Random.Range(1.0f, 3.0f);
        isAttacking = true;
        anim.GetComponent<Animation>().Play("LoopAttack");
        yield return new WaitForSeconds(5 + waitTime);
        isAttacking = false;
    }
}