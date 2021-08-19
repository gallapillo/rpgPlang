using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4;// Скорость персонажа 

    private Rigidbody2D myRigidbody2D;// Твердое тело
    private Vector3 change;
    private Animator animator;
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;


    // Во время старта
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", - 1);
        transform.position = startingPosition.initalValue;
    }

    // Во время каждого кадра
    void Update()
    {
        change = Vector3.zero;
        // Берем управление
        change.x = Input.GetAxis("Horizontal");
        change.y = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)//&& currentState != PlayerState.stagger
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
        
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.idle;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
            
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody2D.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knocktime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knocktime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    private IEnumerator KnockCo(float knocktime)
    {
        if (myRigidbody2D != null)
        {
            yield return new WaitForSeconds(knocktime);
            myRigidbody2D.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody2D.velocity = Vector2.zero;
        }
    }

}
