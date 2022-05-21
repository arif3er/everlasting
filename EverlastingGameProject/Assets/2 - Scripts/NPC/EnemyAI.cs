using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform enemyGFX;
    public Transform attackPoint;
    public Animator animator;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float attackRange;
    public float aggroDistance;

    private Path path;
    int currentWaypoint = 0;
    bool isReachedEndOfPath = false;
    private bool isAggresive = false;
    private bool isAttacking = false;

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
            isReachedEndOfPath = true;
            return;
        } else
        {
            isReachedEndOfPath = false;
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
        // Dusmandan player'a dogru bir cizgi ceker,
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroDistance);

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

        if ((Vector2.Distance(transform.position, player.position) < attackRange))
        {
            Invoke("Attack",1);
            //animasyonu 1 kere oynatcak
            isAttacking = true;
        }
    }

    void Attack()
    {

        if ((Vector2.Distance(transform.position, player.position) < attackRange))
        {
            //damageplayer
        }

        isAttacking = false;
    }




}
