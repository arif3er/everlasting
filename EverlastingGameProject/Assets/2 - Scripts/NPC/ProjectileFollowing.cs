using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollowing : MonoBehaviour
{
    public float speed;             // Merminin hareket hizi
    public float startFollowTime;   // Merminin havadaki yasam suresi (baslangicta ayarlanan referans degisken)
    private float followTime;       // Merminin havadaki yasam suresi (iceride degistirilen degisken)

    public float minDamage;
    public float maxDamage;

    private Transform player;       // Player'in koordinati
    private CharacterStats characterStats;

    private Animator animator;
    
    private void OnEnable()
    {
        Invoke("DisableProjectile", 2f);
    }

    void Start()
    {
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        followTime = startFollowTime;
    }

    void Update()
    {
        animator.SetBool("OnAir", true);

        // Mermi Player'a dogru g�d�ml� olarak hareket eder.
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Havadaki yasam suresini tamamladiysa mermi silinir, 
        if (followTime <= 0)
        {
            animator.Play("ProjectileBlow");
            Invoke("DestroyProjectile", 0.2f);

            followTime = startFollowTime;
        } else
        {
            followTime -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characterStats.TakeDamage(minDamage,maxDamage);
            animator.Play("ProjectileBlow");
            Invoke("DestroyProjectile", 0.2f);
        } else if (collision.CompareTag("Obstacle"))
        {
            animator.Play("ProjectileBlow");
            Invoke("DestroyProjectile", 0.2f);
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
