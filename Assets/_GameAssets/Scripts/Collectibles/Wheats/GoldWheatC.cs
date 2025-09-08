using UnityEngine;

public class GoldWheatC : MonoBehaviour , ICollectible
{
    
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private MoveController _moveController;


    public void Collect()
    {
       _moveController.setMovemetSpeed(_wheatDesignSO.IncreaseDecraseMultiplier, _wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }


}
