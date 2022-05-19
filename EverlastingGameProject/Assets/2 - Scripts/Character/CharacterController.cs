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
   
   public Animator animator;

   public void Start()
   {
      myRB = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0.0f);
      
      animator.SetFloat("Horizontal",movement.x);         //Yatay düzlemde hareket girdisi.
      animator.SetFloat("Vertical",movement.y);           //Dikey düzlemde hareket girdisi.
      animator.SetFloat("Magnitude",movement.magnitude);  //Hareket ediyor kontrolü.
      
      transform.position = transform.position + movement * Time.deltaTime * characterSpeed;  //Hareket metodu.
      
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
}
