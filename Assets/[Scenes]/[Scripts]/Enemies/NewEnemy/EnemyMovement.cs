using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public float range;
    public Transform centerPoint;
    public float detectionRange;
    public Transform player;

    public bool canRun = true;

    private bool isAttacking = false;

    private float groanTimer = 0;
    private float groanRange = 0;

    private float distance;

    EnemyAnimController animController;

    void Start()
    {
        animController = GetComponent<EnemyAnimController>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        groanTimer += Time.deltaTime;

        if (groanTimer >= groanRange)
        {

            GetComponent<FMODUnity.StudioEventEmitter>().Play();

            groanRange = Random.Range(5, 15);
            groanTimer = 0;
        }

        if (!isAttacking)
        {
            // If player detected/in range
            if (GetDistance() < detectionRange)
            {
                agent.SetDestination(player.position);
                // Can enemy play run animation
                if (canRun)
                {
                    animController.StateSelector = EnemyAnimController.AnimState.Chase;
                    agent.speed = 8f;
                }
            }
            else
            {
                animController.StateSelector = EnemyAnimController.AnimState.Wander;
                agent.speed = 1.5f;
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Vector3 point;
                    if (RandomPoint(centerPoint.position, range, out point))
                    {
                        agent.SetDestination(point);
                    }
                }
            }
        }
        else
            agent.SetDestination(transform.position);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public float GetDistance()
    {
        distance = Vector3.Distance(transform.position, player.position);
        return distance;
    }

    public void SetAttacking(bool state)
    {
        isAttacking = state;
    }
}