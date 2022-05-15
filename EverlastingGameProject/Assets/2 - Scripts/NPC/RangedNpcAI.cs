using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNpcAI : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minDistance;

    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;

    private void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

        if (Vector2.Distance(transform.position, target.position) < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
        else
        {
            // ATTACK
        }
    }
}
