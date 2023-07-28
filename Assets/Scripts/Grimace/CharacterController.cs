using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private Animator animator;
    private Rigidbody _rb;
    private Vector3 _movement;
    public bool _isMoving = false;
    private bool isAttacking;
    private bool isJumping;
    private CameraController _cameraController;

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
            if (!_isMoving)
            {
                _isMoving = true;
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            if (_isMoving)
            {
                _isMoving = false;
                print("Cancel current action From movement");
            }
        }
        UpdateAnimator(hasInput);
    }

    private void UpdateAnimator(bool hasInput)
    {
        animator.SetBool("isRunning", hasInput);
    }

    private void FixedUpdate()
    {
        //if (!_isMoving) return;

        if(!isAttacking) Move();
        Rotate();
        Attack();
    }

    private void Move()
    {
        Vector3 newPosition = _rb.position + _movement * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }

    private void Jump()
    {

    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            isAttacking = true;
            animator.SetBool("isScaring", true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            isAttacking = false;
            animator.SetBool("isScaring", false);
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
}
