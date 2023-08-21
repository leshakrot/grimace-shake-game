using System.Collections;
using UnityEngine;
using YG;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;

    public GameOverPopupController popup;
    public Vector3 startPosition;

    public float moveSpeed;
    public bool isMoving = false;

    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem screamFX;
    [SerializeField] private PedestrianController pedController;
    [SerializeField] private UI ui;
    [SerializeField] private CameraController _cameraController;

    [SerializeField] private GameObject _mobileControl;
    [SerializeField] private GameObject _mobileControlPanel;
    [SerializeField] private GameObject _pcControl;
    [SerializeField] private GameObject _joystick;

    private Rigidbody _rb;
    private Vector3 _movement;
    private bool isAttacking;
    private bool isJumping;

    private bool isMobile;

    [Header("Sounds")]
    [SerializeField] private AudioSource _screamSound;
    [SerializeField] private AudioSource _slurpSound;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        instance = this;

        _rb = GetComponent<Rigidbody>();

        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    public void GetData()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            _mobileControl.SetActive(true);
            _mobileControlPanel.SetActive(true);
            _joystick.SetActive(true);
            isMobile = true;
        }
        else
        {
            _pcControl.SetActive(true);
            //Cursor.lockState = CursorLockMode.Locked;
        }
        Debug.Log("PC " + YandexGame.EnvironmentData.isDesktop);
        Debug.Log("Mobile " + YandexGame.EnvironmentData.isMobile);
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isMobile) Scare();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        if (popup.isRespawned)
        {
            transform.position = startPosition;

            HideCursor();

            popup.gameObject.SetActive(false);

            popup.isRespawned = false;

            transform.position = startPosition;
        }
    }

    public void HideCursor()
    {
        if (!YandexGame.EnvironmentData.isMobile)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
        if (!isJumping && !isAttacking)
        {
            animator.SetTrigger("Scare");
            _screamSound.PlayDelayed(0.5f);
            screamFX.gameObject.SetActive(true);
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
        screamFX.gameObject.SetActive(false);
        isAttacking = false;
    }

    IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAttacking)
        {
            if (other.gameObject.TryGetComponent(out Pedestrian ped))
            {
                StartCoroutine(WaitBeforeRunaway(ped));
                ui.AddTerrifiedCount();
                ui.UpdateSlider();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Shake shake))
        {
            _slurpSound.Play();
            ui.AddShakesCount();
            shake.isCollected = true;
        }
    }

    IEnumerator WaitBeforeRunaway(Pedestrian ped)
    {
        yield return new WaitForSeconds(1f);
        ped.gameObject.GetComponent<Pedestrian>().isTerrified = true;
        ped.gameObject.GetComponentInChildren<Animator>().SetTrigger("Terrified");
    }
}
