using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;

    private Rigidbody _rb;
    private Vector3 _movement;
    public bool isMoving = false;
    private bool isAttacking;
    private bool isJumping;
    private CameraController _cameraController;

    [Header("Sounds")]
    [SerializeField] private AudioSource _screamSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        float horizontalInput = SimpleInput.GetAxis("Horizontal");
        float verticalInput = SimpleInput.GetAxis("Vertical");
        bool hasInput = horizontalInput != 0 || verticalInput != 0;

        _movement = Quaternion.Euler(0f, _cameraController.transform.eulerAngles.y, 0f) * new Vector3(horizontalInput, 0f, verticalInput);
        _movement = _movement.normalized;

        if (hasInput)
        {
            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                print("Cancel current action From movement");
            }
        }
        UpdateAnimator(hasInput);
        if (Input.GetKeyDown(KeyCode.Tab)) Scare();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void UpdateAnimator(bool hasInput)
    {
        animator.SetBool("isRunning", hasInput);
    }

    private void FixedUpdate()
    {
        //if (!_isMoving) return;

        if (!isAttacking) Move();
        Rotate();
        //Jump();
    }

    private void Move()
    {
        Vector3 newPosition = _rb.position + _movement * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    public void Jump()
    {
        if (isMoving) animator.SetTrigger("RunJump");
        else animator.SetTrigger("StandJump");
        StartCoroutine(JumpCooldown());
    }

    public void AddForceToJump()
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Scare()
    {
        if (!isJumping)
        {
            animator.SetTrigger("Scare");
            _screamSound.PlayDelayed(0.5f);
            StartCoroutine(AttackCooldown());
        }
    }

    private void Rotate()
    {
        if (_movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_movement);
            _rb.MoveRotation(Quaternion.Lerp(_rb.rotation, toRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }

    IEnumerator AttackCooldown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(3f);
        isAttacking = false;
    }

    IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }
}
