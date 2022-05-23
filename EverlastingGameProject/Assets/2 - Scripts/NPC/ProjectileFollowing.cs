using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollowing : CharacterStats
{
    public float speed;             // Merminin hareket hizi
    public float startFollowTime;   // Merminin havadaki yasam suresi (baslangicta ayarlanan referans degisken)
    private float followTime;       // Merminin havadaki yasam suresi (iceride degistirilen degisken)

    public float minDamage;
    public float maxDamage;

    private Transform player;       // Player'in koordinati
    public CharacterStats characterStats;
    
    private void OnEnable()
    {
        Invoke("DisableProjectile", 2f);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        followTime = startFollowTime;
    }

    void Update()
    {
        // Mermi Player'a dogru g�d�ml� olarak hareket eder.
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Havadaki yasam suresini tamamladiysa mermi silinir, 
        if (followTime <= 0)
        {
            DisableProjectile();

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
            DisableProjectile();
        } else if (collision.CompareTag("Obstacle"))
        {
            DisableProjectile();
        }
    }

    private void DisableProjectile()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
