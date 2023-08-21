using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // ������ ����� ��������������
    public float detectionDistance = 10f; // ����������, �� ������� ���� ������������ ������
    public float attackDistance = 3f; // ����������, �� ������� ���� ������� ������
    public Kicker kicker;

    [SerializeField] private Transform player;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int currentPatrolPointIndex;
    private float lastAttackTime;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        currentPatrolPointIndex = 0;
        SetNextPatrolPoint();
    }

    void Update()
    {
        // ���������� �� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log(distanceToPlayer);
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
                StartCoroutine(WaitBeforePlayerDie()); 
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

    private IEnumerator WaitBeforePlayerDie()
    {
        yield return new WaitForSeconds(0.5f);
        player.position = CharacterController.instance.startPosition;
        kicker.Attack();
    }
}
