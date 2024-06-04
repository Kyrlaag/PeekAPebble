using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private SearchState searchState;
    [SerializeField] private float playerMaxDistance;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public override State RunCurrentState()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= playerMaxDistance)
        {
            if (searchState != null)
            {
                transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("walk");
            }
            else
            {
                transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("chase");
            }
            return chaseState;
        }
        else if (searchState != null)
        {
            transform.parent.parent.GetComponent<EnemyAIController>().SetAITarget(null);
            return searchState;

        }
        else
        {
            return this;

        }
    }
}
