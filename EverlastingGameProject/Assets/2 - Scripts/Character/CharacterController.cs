using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
   
   
   public float speedBuff = 4f;
   private float currentSpeed;
   
   private float attackTime = .25f;
   private float attackCounter = .25f;
   private bool isAttacking;
   
   private Rigidbody2D myRB;
   private Vector2 movement;
   public Animator animator;

   private float direction;
   [SerializeField]
   private CharacterStats characterStats;
   public Transform attackPoint;
   public Transform attackPointRight;
   public Transform attackPointLeft;
   public float attackRange = 0.5f;
   public LayerMask enemyLayers;

   
   

   public void Start()
   {
      currentSpeed = characterStats.characterSpeed;
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
      
      
      if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical")==1 || Input.GetAxisRaw("Vertical")== -1)
      {
         animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
         animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
         direction = Input.GetAxisRaw("Horizontal");
         if (direction <= 0)
         {
            attackPoint.position = attackPointLeft.position;
         }
         else
         {
            attackPoint.position = attackPointRight.position;
         }
      }

      if (Input.GetKey(KeyCode.Space))
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
      if (Input.GetKeyDown(KeyCode.F1))
      {
         ChangeWeapon();
      }

      if (Input.GetKey(KeyCode.LeftShift))
      {
         characterStats.characterSpeed = speedBuff;
      }
      else
      {
         characterStats.characterSpeed = currentSpeed;
      }

      
      
   }

   private void FixedUpdate()
   {
      myRB.MovePosition(myRB.position + movement * characterStats.characterSpeed * Time.fixedDeltaTime);
   }

   private void ChangeWeapon()
   {
      if (animator.GetFloat("ChangeWeapon") == -1f)
      {
         animator.SetFloat("ChangeWeapon", 1f);
      }
      else
      {
         animator.SetFloat("ChangeWeapon", -1f); 
      }
   }

   public void Attack()
   {
      
      
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
      foreach (Collider2D enemy in hitEnemies)
      {
         enemy.GetComponent<NpcStats>().TakeDamage(characterStats.minAttackDamge, characterStats.maxAttackDamge);
         
         
      }
   }

   private void OnDrawGizmosSelected()
   {
      if (attackPoint == null)
      {
         return;
      }
      Gizmos.DrawWireSphere(attackPoint.position,attackRange);
   }
}


