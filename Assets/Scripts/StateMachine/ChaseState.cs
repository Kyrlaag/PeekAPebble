using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    [SerializeField] private IdleState idleState;
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private float playerMaxDistance;
    private Transform player;
    [SerializeField] private bool isBanshee;
    private bool canPlayAudio = true;
    private bool didLeaveChase = true;
    private float waitBtwAudio = 2;
    private float counterWaitBtwAudio;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }


    public override State RunCurrentState()
    {
        if (didLeaveChase == true)
        {
            if (canPlayAudio == true)
            {
                PlayChaseAudio();
            }
            else
            {
                WaitForNextAudio();
            }
        }
        
        
        if (Vector2.Distance(player.position, transform.position) <= playerMaxDistance) 
        {
            StopAllCoroutines();
            transform.parent.parent.gameObject.GetComponent<EnemyAIController>().SetAITarget(player);
            return this;
        }
        else
        {
            transform.parent.parent.gameObject.GetComponent<EnemyAIController>().SetAITarget(null);

            didLeaveChase = true;

            if (patrolState != null) 
            {
                transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("walk");
                return patrolState;
            }
            else
            {
                transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("idle");
                return idleState;
            }
        }

    }

    private void PlayChaseAudio()
    {
        didLeaveChase = false;
        canPlayAudio = false;

        if (isBanshee == true)
        {
            FindObjectOfType<AudioManager>().Play("BansheeScream");

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("DemonChase");

        }
        counterWaitBtwAudio = waitBtwAudio;
    }

    void WaitForNextAudio()
    {
        if (counterWaitBtwAudio <= 0)
        {
            canPlayAudio = true;

        }
        else
        {
            counterWaitBtwAudio -= Time.deltaTime;
        }
    }



}
