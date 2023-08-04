using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // ������ ����� ��������������
    public float detectionDistance = 10f; // ����������, �� ������� ���� ������������ ������
    public float attackDistance = 3f; // ����������, �� ������� ���� ������� ������


    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform player;
    private int currentPatrolPointIndex;
    private float lastAttackTime;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPatrolPointIndex = 0;
        SetNextPatrolPoint();
    }

    void Update()
    {
        // ���������� �� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ���� ����� ��������� �� ��������� �����
        if (distanceToPlayer <= detectionDistance)
        {
            // ���������� �������������� � ������ � ������
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.speed = 4.5f;
            animator.SetBool("isRunning", true);

            // ���� ����� ���������� ������, ����� ���������
            if (distanceToPlayer <= attackDistance)
            {
                navMeshAgent.speed = 0f;
                // ����������� �������� ����� � �������� ����� �����
                animator.SetTrigger("Punch");
            }
        }
        else
        {
            // ���� ����� �� �����, ���������� ��������������
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                navMeshAgent.speed = 1.5f;
                SetNextPatrolPoint();
            }

            animator.SetBool("isRunning", false);
        }
    }

    void SetNextPatrolPoint()
    {
        animator.SetBool("isWalking", true);
        // �������� ��������� ����� ��������������
        if (patrolPoints.Length > 0)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
        }
    }
}
