using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject gfx;
    public GameObject soundWave;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float throwForce = 10f;
    Rigidbody2D rb;
    Vector2 movement;
     private bool isFaceRight = true;
     private bool isSoundWaving = false;
     private Coroutine test;

    private bool isRock = false;
    private bool canMove = true;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (canMove == true)
        {
            Move();

        }

        if ((movement.x < 0 && isFaceRight) || (movement.x > 0 && isFaceRight == false)){
            Flip();
        }
                
        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        if (Input.GetKeyDown(KeyCode.E))
        {
            TransformEvent();
        }
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    
    private void Flip()
    {
        gfx.transform.localScale = new Vector2(gfx.transform.localScale.x * -1, gfx.transform.localScale.y);
        isFaceRight = !isFaceRight;
    }

    void Move() 
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        if (movement != Vector2.zero && isSoundWaving == false)
        {
           test = StartCoroutine(waitForNextSpawn());       
        }

        else if (movement == Vector2.zero && isSoundWaving == true)
        {
            StopCoroutine(test);
            isSoundWaving = false;
        }
    }

    IEnumerator spawnSoundWave()
    {
        FindObjectOfType<AudioManager>().RandomFootstep();

        GameObject newObject = Instantiate(soundWave, transform.parent);
        newObject.transform.position = transform.position;
        yield return new WaitForSeconds(0.8f);
        Destroy(newObject);
    }

    IEnumerator waitForNextSpawn()
    {
        isSoundWaving = true;
        StartCoroutine(spawnSoundWave());
        yield return new WaitForSeconds(0.25f);
        isSoundWaving = false;
        yield return null;
    }

    private void TransformEvent()
    {
        
        isRock = !isRock;
        canMove = !canMove;
        animator.SetTrigger("transform");

        if (isRock == true) 
        {
            movement = Vector2.zero;
            gameObject.tag = "Obstacle";
        }
        else
        {
            gameObject.tag = "Player";
        }
    }

    
}