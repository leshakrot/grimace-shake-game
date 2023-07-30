using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Pedestrian : MonoBehaviour
{
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
        if (agent.remainingDistance < 0.5f)
        {
            Vector3 randomPoint = GetRandomWanderPoint(transform.position, 100f);
            SetTargetPosition(randomPoint);
        }

        animator.SetBool("isWalking", agent.velocity.magnitude >= 0.5f);
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
