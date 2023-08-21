using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints; // Массив точек патрулирования
    public float detectionDistance = 10f; // Расстояние, на котором враг обнаруживает игрока
    public float attackDistance = 3f; // Расстояние, на котором враг атакует игрока
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
        // Расстояние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log(distanceToPlayer);
        // Если игрок находится на видимости врага
        if (distanceToPlayer <= detectionDistance)
        {
            // Остановить патрулирование и бежать к игроку
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.speed = 4.5f;
            animator.SetBool("isRunning", true);

            // Если игрок достаточно близко, чтобы атаковать
            if (distanceToPlayer <= attackDistance)
            {
                navMeshAgent.speed = 0f;
                // Проигрываем анимацию атаки и вызываем метод атаки
                animator.SetTrigger("Punch");
                StartCoroutine(WaitBeforePlayerDie()); 
            }
        }
        else
        {
            // Если игрок не виден, продолжаем патрулирование
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
        // Выбираем следующую точку патрулирования
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
