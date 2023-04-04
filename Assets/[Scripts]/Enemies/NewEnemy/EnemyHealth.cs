using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool isPawn;
    [SerializeField] private bool isRook;

    private float health;

    EnemyMovement em;

    void Start()
    {
        em = GetComponent<EnemyMovement>();
        SetHealth();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            em.SetAttacking(true);
            GetComponent<EnemyController>().Die();
        }
    }

    void SetHealth()
    {
        if (isPawn)
            health = 100f;
        else if (isRook)
            health = 300f;
    }
}