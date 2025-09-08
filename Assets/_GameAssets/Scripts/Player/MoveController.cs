using System;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private PlayerController _playerController;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 15f;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private float _startMovementSpeed, _startingJumpForce;

    [SerializeField] private KeyCode _movementKey;

    //---------------------------------------------------//
    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpkey;
    [SerializeField] private float _jumpforce;
    [SerializeField] private bool _canjump = true;
    [SerializeField] private float _jumpCooldown;
    //--------------------------------------------------//
    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;
    private bool _isSliding;

    [Header("Sliding Settiings")]

    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _slideDrag;


    public event Action<bool> OnSlidingStateChanged;
    private bool _lastSlidingState;


    public bool IsSliding()
    {
        return _isSliding;
    }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _startMovementSpeed = _moveSpeed;
        _startingJumpForce = _jumpforce;

    }

    public void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        bool prevSliding = _isSliding;

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }

        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }

        else if (Input.GetKeyDown(_jumpkey) && _canjump && IsGrounded())
        {
            setJump();
            _canjump = false;
            Invoke(nameof(resetJumping), _jumpCooldown);
            AudioManager.Instance.Play(SoundType.JumpSound);
        }

        if (_isSliding != prevSliding)
        {
            OnSlidingStateChanged?.Invoke(_isSliding);
        }
 
    }

    public void PlayerMovement()
    {
        _movementDirection = _playerController.OrientationTransform.forward * _verticalInput + _playerController.OrientationTransform.right * _horizontalInput;

        if (_isSliding == true)
        {
            _playerController.PlayerRigidbody.AddForce(_movementDirection.normalized * _moveSpeed * _slideSpeed, ForceMode.Force);
        }
        else
        {
            _playerController.PlayerRigidbody.AddForce(_movementDirection.normalized * _moveSpeed, ForceMode.Force);
        }
    }

    public void setPlayerDrag()
    {
        if (_isSliding)
        {
            _playerController.PlayerRigidbody.linearDamping = _slideDrag;
        }

        else
        {
            _playerController.PlayerRigidbody.linearDamping = _groundDrag;
        }
    }

    public void limitSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerController.PlayerRigidbody.linearVelocity.x, 0f, _playerController.PlayerRigidbody.linearVelocity.z);

        if (flatVelocity.magnitude > _moveSpeed)
        {
            Vector3 LimitedVelocity = flatVelocity.normalized * _moveSpeed;
            _playerController.PlayerRigidbody.linearVelocity = new Vector3(LimitedVelocity.x, _playerController.PlayerRigidbody.linearVelocity.y, LimitedVelocity.z);
        }

    }

    public void setJump()
    {

        _playerController.PlayerRigidbody.AddForce(transform.up * _jumpforce, ForceMode.Impulse);


    }
    public void resetJumping()
    {
        _canjump = true;
    }


    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    public bool IsMoving()
    {
        return Mathf.Abs(_horizontalInput) > 0.1f || Mathf.Abs(_verticalInput) > 0.1f;
    }


    public void setMovemetSpeed(float newSpeed, float duration)
    {
        _moveSpeed += newSpeed;
        Invoke(nameof(resetMovementSpeed), duration);


    }

    public void resetMovementSpeed()
    {
        _moveSpeed = _startMovementSpeed;

    }
    
    public void setJumpForce(float newJumpForce, float duration)
    {
        _jumpforce += newJumpForce;
        Invoke(nameof(resetJumpForce), duration);
    }
    
    public void resetJumpForce()
    {
        _jumpforce = _startingJumpForce;
    }


}
