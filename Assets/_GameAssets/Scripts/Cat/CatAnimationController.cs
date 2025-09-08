using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _catAnimator;

    private CatStateController _catStateController;

    private void Awake()
    {
        _catStateController = GetComponent<CatStateController>();
    }

    private void Update()
    {
        SetCatAnimation();
    }

    private void SetCatAnimation()
    {
        var currentState = _catStateController.GetCurrentState();

        // Önce tüm parametreleri false yap
        _catAnimator.SetBool("IsWalking", false);
        _catAnimator.SetBool("IsRunning", false);
        _catAnimator.SetBool("IsIdling", false);
        _catAnimator.SetBool("IsAttacking", false);

        // Sonra sadece ilgili olanı true yap
        switch (currentState)
        {
            case CatState.Idle:
                _catAnimator.SetBool("IsIdling", true);
                break;
            case CatState.Walking:
                _catAnimator.SetBool("IsWalking", true);
                break;
            case CatState.Running:
                _catAnimator.SetBool("IsRunning", true);
                break;
            case CatState.Attacking:
                _catAnimator.SetBool("IsAttacking", true);
                break;
        }
    }
}