using UnityEngine;
using UnityEngine.AI;

public class Pedestrian : MonoBehaviour
{
    public bool isTerrified;
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.5f && !isTerrified)
        {
            Vector3 randomPoint = GetRandomWanderPoint(transform.position, 100f);
            SetTargetPosition(randomPoint);
        }

        if (!isTerrified) animator.SetBool("isWalking", agent.velocity.magnitude >= 0.5f);
        else agent.speed *= 1.1f;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        agent.SetDestination(targetPosition);
    }

    private Vector3 GetRandomWanderPoint(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas);
        return hit.position;
    }
}
