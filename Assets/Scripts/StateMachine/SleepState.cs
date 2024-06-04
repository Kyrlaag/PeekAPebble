using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : State
{
    // dusmanin oyuncuyu farketme durumu
    private GameObject player;
    private bool isWakingUp = false;
    private bool isWokeUp = false;

    [SerializeField] private State idleState;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public override State RunCurrentState()
    {
        if (isWakingUp == false && Vector2.Distance(player.transform.position, transform.position) <= 6f)
        {
            isWakingUp = true;
            StartCoroutine(WaitForWakeUp());

        }

        if (isWokeUp == true)
        {
            return idleState;
        }
        else
        {
            return this;
        }
    }

    private IEnumerator WaitForWakeUp()
    {
        PlayRandomAudio();
        transform.parent.parent.gameObject.GetComponent<EnemyAnimationController>().ChangeAnimation("wakeUp");
        yield return new WaitForSeconds(1f);
        isWokeUp = true;
    }

    void PlayRandomAudio()
    {

        int randInt = Random.Range(1, 2);

        string randString = "WakeUp_" + randInt.ToString();
        Debug.Log(randString);
        FindObjectOfType<AudioManager>().Play(randString);

        //Play(randomSFX[sfxIndex]);

    }
}
