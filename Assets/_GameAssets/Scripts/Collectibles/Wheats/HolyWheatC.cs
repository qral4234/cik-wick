using UnityEngine;

public class HolyWheatC : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private MoveController _moveController;


    public void Collect()
    {
        _moveController.setJumpForce(_wheatDesignSO.IncreaseDecraseMultiplier, _wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
    

}
