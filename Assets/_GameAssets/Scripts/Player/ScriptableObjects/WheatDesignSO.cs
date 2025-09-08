using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesignSO : ScriptableObject 
{
    [SerializeField] private float _increaseDecraseMultiplier;
    [SerializeField] private float _resetBoostDuration;

    public float IncreaseDecraseMultiplier => _increaseDecraseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
}
