using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform OrientationTransform => _orientationTransform;
    public Rigidbody PlayerRigidbody => _playerRigidbody;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 180f; // Derece/saniye

    [SerializeField] private Transform _orientationTransform;
    private Rigidbody _playerRigidbody;
    private MoveController _moveController;
    private PlayerAnimationController _playerAnimationController;



    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
        _moveController = GetComponent<MoveController>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }



        _moveController.SetInputs();
        _moveController.setPlayerDrag();
        _moveController.limitSpeed();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        // İleri-geri hareket
        float vertical = Input.GetAxis("Vertical");
        transform.position += transform.forward * vertical * moveSpeed * Time.deltaTime;

        // Sağa-sola dönüş
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);


        _moveController.PlayerMovement();
    }


    public bool CanCatChase()
    {
        // Raycast ile aşağıya bak, Floor tag'li zemine temas varsa true döner
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            return hit.collider.CompareTag("Floor");
        }
        return false;
    }
}

