using UnityEngine;

public class EggCollectibles : MonoBehaviour ,ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
