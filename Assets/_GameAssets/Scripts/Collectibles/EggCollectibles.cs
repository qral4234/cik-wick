using UnityEngine;

public class EggCollectibles : MonoBehaviour ,ICollectible
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
    }
}
