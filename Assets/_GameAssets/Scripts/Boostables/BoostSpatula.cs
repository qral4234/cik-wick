using UnityEngine;

public class BoostSpatula : MonoBehaviour, IBoostables
{
    [SerializeField] private Animator _spatulaAnimator;
    [SerializeField] private float _jumpforce;

    private bool _isActive;

    public void boost(PlayerController _playerController)
    {
        if (_isActive) return;

        Rigidbody playerRigidbody = _playerController.PlayerRigidbody;

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpforce, ForceMode.Impulse);

        PlayBoostAnimation();

        _isActive = true;
        Invoke(nameof(ResetActive), 0.2f);
        AudioManager.Instance.Play(SoundType.SpatulaSound);

    }


    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger("IsSpatulaJumping");
        
    }


    private void ResetActive()
    {
        _isActive = false;
    }


}