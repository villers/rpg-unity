using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float attackRange = 3.0f;
    public float attackCooldown = 1.0f;
    private float attackTimer = 0.0f;

    private NavMeshAgent agent;
    private EntityStats enemyStats;
    private GameObject player;
    private EntityStats playerStats;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EntityStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<EntityStats>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                enemyStats.Attack(playerStats);
                attackTimer = attackCooldown;
            }
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
