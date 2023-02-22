using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public Joystick joystick;
    //public floatValue currentHealth;
    //public Signal playerHealthSignal;
    //public VectorValue startingPosition;
    //public Inventory playerInventory;
    //public SpriteRenderer recieveItemSprite;
    //public Signal playerHit;
    //public Signal ReduceMagic;

    //[Header("Projectile Stuff")]
    //public GameObject projectile;
    //public Item bow;

    //[Header("IFrame Stuff")]
    //public Color flashColor;
    //public Color regularColor;
    //public float flashDuration;
    //public int numbersOfFlashes;
    //public Collider2D triggerCollider;
    //public SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX",0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = joystick.Horizontal;
        change.y = joystick.Vertical;
        if(currentState == PlayerState.walk ||currentState == PlayerState.idle)
        {
            UpdateAnimationCharacter();
        }

    }

   
    void MoveCharacter() //method to move
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void UpdateAnimationCharacter()//method on animation
    {
        if(change != Vector3.zero)
        {
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

}
