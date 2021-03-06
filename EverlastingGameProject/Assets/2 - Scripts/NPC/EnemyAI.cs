using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform enemyGFX;
    public Animator animator;
    public CharacterStats characterStats;

    public float minDamage;
    public float maxDamage;
    public float attackSpeed;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float attackRange;
    public float aggroDistance;
    public float stoppingDistance;

    private Path path;
    int currentWaypoint = 0;
    private bool isAggresive = false;
    private bool isCooldown = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        SearchAndAttack();
    }

    private void FixedUpdate()
    {

        if (path == null) return;
        if (isAggresive == false) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dusman?n menziline ait gizmoslar.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void SearchAndAttack()
    {
        if ((Vector2.Distance(transform.position, player.position) > aggroDistance)) 
        {
            isAggresive = false;
        }else
        {
            isAggresive = true;
        }

        if ((Vector2.Distance(transform.position, player.position) < attackRange) && !isCooldown)
        {
            Invoke("Attack", 0.5f);
            Invoke("ResetCooldown", attackSpeed);
            //animasyonu 1 kere oynatcak
            isCooldown = true;
        }
    }

    void Attack()
    {
        if ((Vector2.Distance(transform.position, player.position) < attackRange))
        {
            characterStats.TakeDamage(minDamage,maxDamage);
        }
    }

    void ResetCooldown()
    {
        isCooldown = false;
    }



}
