using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform _playerVisualTransform;

    private PlayerController _playerController;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }

        // --- Booster UI animasyonlarını tetikle ---
        var ui = FindObjectOfType<PlayerStateUI>();
        if (ui != null)
        {
            if (other.CompareTag("GoldWheat"))
            {
                ui.ActivateGoldBoosterUI();
            }
            else if (other.CompareTag("HolyWheat"))
            {
                ui.ActivateHolyBoosterUI();
            }
            else if (other.CompareTag("RottenWheat"))
            {
                ui.ActivateRottenBoosterUI();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostables>(out var boostable))
        {
            boostable.boost(_playerController);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform);
        }
    }

}