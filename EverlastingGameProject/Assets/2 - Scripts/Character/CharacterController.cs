using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
   [SerializeField] private float characterSpeed = 2f;
   private float attackTime = .25f;
   private float attackCounter = .25f;
   private bool isAttacking;
   
   private Rigidbody2D myRB;
   private Vector2 movement;
   public Animator animator;

   public void Start()
   {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in arr)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                Physics2D.IgnoreCollision(arr[i].GetComponent<Collider2D>(), arr[j].GetComponent<Collider2D>());
            }
        }

        myRB = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      movement.x = Input.GetAxis("Horizontal");
      movement.y = Input.GetAxis("Vertical");
      
      animator.SetFloat("Horizontal",movement.x);         //Yatay düzlemde hareket girdisi.
      animator.SetFloat("Vertical",movement.y);           //Dikey düzlemde hareket girdisi.
      animator.SetFloat("Magnitude",movement.magnitude);  //Hareket ediyor kontrolü.
      
      //transform.position = transform.position + movement * Time.deltaTime * characterSpeed;  //Hareket metodu.
      
      if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical")==1 || Input.GetAxisRaw("Vertical")== -1)
      {
         animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
         animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));

      }
      

      if (Input.GetKeyDown(KeyCode.Space))
      {
         attackCounter = attackTime;
         animator.SetBool("Attack",true);
         isAttacking = true;
      }
      if(isAttacking){
         myRB.velocity = Vector2.zero;
         attackCounter -= Time.deltaTime;
         if (attackCounter <= 0)
         {
            animator.SetBool("Attack",false);
            isAttacking = false;
         }

      }
   }

   private void FixedUpdate()
   {
      myRB.MovePosition(myRB.position + movement * characterSpeed * Time.fixedDeltaTime);
   }
}
