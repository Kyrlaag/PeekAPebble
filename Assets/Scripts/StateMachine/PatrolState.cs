using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolState : State
{
    [SerializeField] private GameObject[] patrolPoints;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private float playerMaxDistance;

    private AIPath aiPath;
    private bool isPatroling = false;
    private GameObject player;
    private int patrolIndex = 0;

    void Start()
    {
        aiPath = transform.parent.parent.GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");

    }


    public override State RunCurrentState()
    {
        if (isPatroling == false)
        {
            transform.parent.parent.GetComponent<EnemyAIController>().SetAITarget(patrolPoints[patrolIndex].transform);
            transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("walk");
            isPatroling = true;
        }

        if (Vector2.Distance(player.transform.position, transform.position) <= playerMaxDistance)
        {
            transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("chase");
            return chaseState;
        }


        if (aiPath.reachedDestination)
        {
            nextPatrolPoint();
        }

        return this;
    }

    private void nextPatrolPoint()
    {
        patrolIndex++;

        if (patrolIndex >= patrolPoints.Length)
            patrolIndex = 0;
        transform.parent.parent.GetComponent<EnemyAIController>().SetAITarget(patrolPoints[patrolIndex].transform);
    }
}
