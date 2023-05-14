using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    private EntityStats characterStats;
    private EntityStats currentTarget;
    private float attackTimer = 0.0f;
    private bool isMouseHeld = false;

    void Start()
    {
        characterStats = GetComponent<EntityStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        HandleMouseInput();
        HandleAttack();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseHeld = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;
        }

        if (isMouseHeld)
        {
            HandleClickOrHold();
        }
    }

    private void HandleClickOrHold()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Enemy") && Input.GetMouseButtonDown(0))
            {
                currentTarget = hitObject.GetComponent<EntityStats>();
            }
            else if (!hitObject.CompareTag("Enemy"))
            {
                currentTarget = null;
                agent.SetDestination(hit.point);
            }
        }
    }

    private void HandleAttack()
    {
        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distanceToTarget <= characterStats.attackRange)
            {
                agent.isStopped = true;
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0)
                {
                    characterStats.Attack(currentTarget);
                    attackTimer = 1f / characterStats.attackSpeed;
                }
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }
}
