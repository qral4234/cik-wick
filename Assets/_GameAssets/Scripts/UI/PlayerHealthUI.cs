using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private Image[] _playerHealthImages;

    [Header("Sprites")]
    [SerializeField] private Sprite _PlayerHealthySprite;
    [SerializeField] private Sprite _PlayerUnhealthySprite;

    [Header("DOTween Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealthTransform;

    private void Awake()
    {
        _playerHealthTransform = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransform[i] = _playerHealthImages[i].GetComponent<RectTransform>();
        }
    }


    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _PlayerHealthySprite)
            {
                AnimatedDamageSprite(_playerHealthImages[i], _playerHealthTransform[i]);
                break;
            }
        }
    }

    public void AnimatedamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            AnimatedDamageSprite(_playerHealthImages[i], _playerHealthTransform[i]);  
        }
    }

    private void AnimatedDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _PlayerUnhealthySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }


}
