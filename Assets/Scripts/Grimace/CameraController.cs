using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _minYAngle = -20f;
    [SerializeField] private float _maxYAngle = 80f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private LayerMask _collisionLayer;

    private float _currentX = 20f;
    private float _currentY = 20f;
    private float _originalDistance;

    private void Start()
    {
        _originalDistance = _distance;
    }

    private void FixedUpdate()
    {
        HandleCameraInput();

        RaycastHit hit;
        if (Physics.SphereCast(_target.position, 0.2f, -transform.forward, out hit, _distance, _collisionLayer))
        {
            // Используем сглаживание для расстояния камеры
            _distance = Mathf.Lerp(_distance, hit.distance, Time.deltaTime * _smoothSpeed);
        }
        else _distance = Mathf.Lerp(_distance, _originalDistance, Time.deltaTime * _smoothSpeed);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -_distance);
        Vector3 position = rotation * negDistance + _target.position;
        transform.rotation = rotation;
        transform.position = position;
    }


    private void HandleCameraInput()
    {
        float horizontalInput = SimpleInput.GetAxis("Horizontal");
        float verticalInput = SimpleInput.GetAxis("Vertical");
        bool hasInput = horizontalInput != 0 || verticalInput != 0;

        if (hasInput)
        {
            _currentX += horizontalInput * _rotationSpeed;
            _currentY -= verticalInput * _rotationSpeed;
            _currentY = Mathf.Clamp(_currentY, _minYAngle, _maxYAngle);
        }
    }
}
