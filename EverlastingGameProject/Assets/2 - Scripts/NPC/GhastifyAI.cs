using UnityEngine;
using Pathfinding;

public class GhastifyAI : MonoBehaviour
{
    private Transform player;
    public Transform enemyGFX;
    public Animator animator;
    private CharacterStats characterStats;
    private NpcStats npcStats;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float aggroDistance;
    public float attackRange;
    public float attackSpeed;
    public float attackSlowness;

    private Path path;
    private int currentWaypoint = 0;

    private bool isDead = false;
    private bool isAggresive = false;
    private bool isCooldown = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    public AudioClip[] npcSounds;
    private AudioSource audioSource;

    private void Start()
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obj in arr)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        npcStats = GetComponent<NpcStats>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponentInChildren<AudioSource>();

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
        isDead = npcStats.isDead;

        SearchAndAttack();
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (path == null)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        if (isAggresive == false)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        animator.SetFloat("Speed", 1);
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
        }
        else
        {
            isAggresive = true;
        }

        if ((Vector2.Distance(transform.position, player.position) < attackRange) && !isCooldown)
        {
            CalculateLookAt();
            animator.Play("Attack");
            PlayAttackSound();
            Invoke("Attack", attackSlowness);
            Invoke("ResetCooldown", attackSpeed);
            isCooldown = true;
        }
    }

    void Attack()
    {
        if ((Vector2.Distance(transform.position, player.position) < attackRange))
        {
            characterStats.TakeDamage(npcStats.minDamage, npcStats.maxDamage);
        }
    }

    void CalculateLookAt()
    {
        if (transform.position.x > player.position.x)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (transform.position.x < player.position.x)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void ResetCooldown()
    {
        isCooldown = false;
    }

    void PlayAttackSound()
    {
        audioSource.clip = npcSounds[0];
        audioSource.PlayOneShot(audioSource.clip); 
    }
}
