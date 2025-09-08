using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private MoveController _moveController;

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
    }

    private void SetPlayerAnimations()
    {
        if (_playerAnimator == null || _moveController == null) return;

        // Önce tüm parametreleri false yap
        _playerAnimator.SetBool("IsSliding", false);
        _playerAnimator.SetBool("IsSlidingActive", false);
        _playerAnimator.SetBool("IsMoving", false);
        _playerAnimator.SetBool("IsJumping", false);

        // Sonra sadece aktif olanı true yap
        if (_moveController.IsSliding())
        {
            _playerAnimator.SetBool("IsSliding", true);
            _playerAnimator.SetBool("IsSlidingActive", true);
        }
        else if (_moveController.IsGrounded())
        {
            if (_moveController.IsMoving())
                _playerAnimator.SetBool("IsMoving", true);
        }
        else
        {
            _playerAnimator.SetBool("IsJumping", true);
        }
    }

    private void Update()
    {
        if(GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
        
        SetPlayerAnimations();
    }
}