using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeNpcAI : MonoBehaviour
{
    [SerializeField] private float speed;
    public Transform target;
    public float minDistance;

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            // ATTACK
        }
    }
}
