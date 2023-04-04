using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private bool isPawn;
    [SerializeField] private bool isRook;

    private bool canAttack = true;

    EnemyAnimController animController;
    EnemyMovement em;

    public FMODUnity.StudioEventEmitter HitNoise;

    void Start()
    {
        animController = GetComponentInParent<EnemyAnimController>();
        em = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && canAttack)
            StartCoroutine(DamagePlayer());

        if (other.CompareTag("Scientist") && canAttack && LevelTwoObjectives.hasScientist)
            StartCoroutine(DamageScientist());
    }

    private IEnumerator DamagePlayer()
    {
        canAttack = false;

        if (isPawn)
        {
            em.SetAttacking(true);
            animController.StateSelector = EnemyAnimController.AnimState.Attack;
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
            yield return new WaitForSeconds(1.0f);

            if (em.GetDistance() < 2.5f)
            {
                FirstPersonController.OnTakeDamage(25);
                HitNoise.Play();
            }

            em.SetAttacking(false);
            animController.StateSelector = EnemyAnimController.AnimState.Chase;
        }
        else if (isRook)
        {
            // Play attack anim
            FirstPersonController.OnTakeDamage(45);
            yield return new WaitForSeconds(1.5f);
        }
        
        canAttack = true;
    }

    private IEnumerator DamageScientist()
    {
        Debug.Log("Attack");
        canAttack = false;

        if (isPawn)
        {
            em.SetAttacking(true);
            animController.StateSelector = EnemyAnimController.AnimState.Attack;
            yield return new WaitForSeconds(0.6575f);
            ScientistBehaviour.health -= 25;
            yield return new WaitForSeconds(0.6575f);
            em.SetAttacking(false);
            animController.StateSelector = EnemyAnimController.AnimState.Chase;
        }
        else if (isRook)
        {
            // Play attack anim
            ScientistBehaviour.health -= 45;
            yield return new WaitForSeconds(1.5f);
        }

        canAttack = true;
    }
}