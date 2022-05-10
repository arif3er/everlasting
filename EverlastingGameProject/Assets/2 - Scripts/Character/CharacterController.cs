using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;
    public SpriteRenderer mySpriteRenderer;
    
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    
    public float runSpeed = 20.0f;
    
    void Start ()
    {
       body = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }
    
    void Update()
    {
       if (Input.GetAxisRaw("Horizontal") == 0 )
       {
          animator.SetBool("MoveRight",false);
          animator.SetBool("MoveLeft",false);
          mySpriteRenderer.flipX = false;
       }
       if (Input.GetAxisRaw("Horizontal") > 0)
       {
          animator.SetBool("MoveRight",true);
       }

       if (Input.GetAxisRaw("Horizontal") < 0)
       {
          animator.SetBool("MoveLeft",true);
          mySpriteRenderer.flipX = true;
       }

       if (Input.GetAxisRaw("Vertical") > 0)
       {
          animator.SetBool("MoveUp",true);
       }
       else
       {
          animator.SetBool("MoveUp",false);
       }
       if (Input.GetAxisRaw("Vertical") < 0)
       {
          animator.SetBool("MoveDown",true);
       }
       else
       {
          animator.SetBool("MoveDown",false);
       }
       
       
       // Gives a value between -1 and 1
       horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
       
       vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }
    
    void FixedUpdate()
    {
       if (horizontal != 0 && vertical != 0) // Check for diagonal movement
       {
          // limit movement speed diagonally, so you move at 70% speed
          horizontal = moveLimiter;
          vertical= moveLimiter;
       } 
    
       body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
