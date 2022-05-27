using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public NpcStats npcStats;
    public GameObject player;
    public CharacterStats characterStats;
    public float speed;

    Vector3 targetPosition;

    private void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = player.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            DestroyProjectile();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            characterStats.TakeDamage(npcStats.minDamage, npcStats.maxDamage);
            DestroyProjectile();
        }
        else if (collision.CompareTag("Obstacle"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
