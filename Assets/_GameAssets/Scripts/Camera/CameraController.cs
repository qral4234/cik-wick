using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _playerVisualTransform;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 3f, -6f); // 3rd person için uygun bir offset
    [SerializeField] private float _followSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    private PlayerController _playerController;
    private Vector3 _velocity;

    private void Awake()
    {
        _playerController = _playerTransform.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
            return;

        // --- KAMERA TAKİBİ ---
        Vector3 desiredPosition = _playerTransform.position + _playerTransform.rotation * _offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, 0.08f);

        // --- KAMERA ROTASYONU ---
        Quaternion desiredRotation = Quaternion.LookRotation(_playerTransform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

        // --- KARAKTER YÖNÜ VE GÖRSELİ ---
        Vector3 viewDirection = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _playerController.OrientationTransform.forward = viewDirection.normalized;

        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = (_playerController.OrientationTransform.forward * _verticalInput + _playerController.OrientationTransform.right * _horizontalInput).normalized;
        if (inputDirection != Vector3.zero)
        {
            _playerVisualTransform.forward = Vector3.Slerp(_playerVisualTransform.forward, inputDirection, _rotationSpeed * Time.deltaTime);
        }
    }
}