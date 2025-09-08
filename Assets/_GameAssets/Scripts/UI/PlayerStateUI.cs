using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStateUI : MonoBehaviour
{
    [Header("Left UI References")]
    [SerializeField] private RectTransform _playerwalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;

    [Header("Left UI Images")]
    [SerializeField] private Image _playerWalkingImage;
    [SerializeField] private Image _playerSlidingImage;

    [Header("Left UI Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;

    [Header("Right Booster UI References")]
    [SerializeField] private RectTransform _goldBoosterTransform;
    [SerializeField] private RectTransform _holyBoosterTransform;
    [SerializeField] private RectTransform _rottenBoosterTransform;

    [Header("Right Booster UI Images (Parent)")]
    [SerializeField] private Image _goldBoosterImage;
    [SerializeField] private Image _holyBoosterImage;
    [SerializeField] private Image _rottenBoosterImage;

    [Header("Right Booster Child Wheat Images")]
    [SerializeField] private Image _goldChildImage;   // Gold (PlayerBoosterSpeed'in child'ı)
    [SerializeField] private Image _holyChildImage;   // Holy (PlayerBoosterJump'ın child'ı)
    [SerializeField] private Image _wheatChildImage;  // Wheat (PlayerBoosterSlow'un child'ı)

    [Header("Booster Parent Sprites")]
    [SerializeField] private Sprite _goldBoosterActiveSprite;
    [SerializeField] private Sprite _goldBoosterPassiveSprite;
    [SerializeField] private Sprite _holyBoosterActiveSprite;
    [SerializeField] private Sprite _holyBoosterPassiveSprite;
    [SerializeField] private Sprite _rottenBoosterActiveSprite;
    [SerializeField] private Sprite _rottenBoosterPassiveSprite;

    [Header("Booster Child Sprites")]
    [SerializeField] private Sprite _boosterPassiveSprite; // Ortak pasif wheat sprite
    [SerializeField] private Sprite _goldActiveSprite;     // Gold toplandığında aktif wheat sprite
    [SerializeField] private Sprite _holyActiveSprite;     // Holy toplandığında aktif wheat sprite
    [SerializeField] private Sprite _wheatActiveSprite;    // Rotten toplandığında aktif wheat sprite

    [Header("DOTween Ayarları")]
    [SerializeField] private float _moveDuration = 0.3f;
    [SerializeField] private Ease _moveEase = Ease.OutQuad;
    [SerializeField] private float _boosterActiveDuration = 5f;

    private MoveController _moveController;

    // Pozisyonlar (kendi UI yerleşimine göre ayarlayabilirsin)
    private Vector2 _walkingActivePos = new Vector2(-20f, 62f);
    private Vector2 _walkingPassivePos = new Vector2(-90f, 62f);
    private Vector2 _slidingActivePos = new Vector2(-20f, -62f);
    private Vector2 _slidingPassivePos = new Vector2(-90f, -62f);

    private Vector2 _goldBoosterActivePos = new Vector2(50f, 125f);
    private Vector2 _goldBoosterPassivePos = new Vector2(90f, 125f);
    private Vector2 _holyBoosterActivePos = new Vector2(50f, 0f);
    private Vector2 _holyBoosterPassivePos = new Vector2(90f, 0f);
    private Vector2 _rottenBoosterActivePos = new Vector2(50f, -125f);
    private Vector2 _rottenBoosterPassivePos = new Vector2(90f, -125f);

    private void Awake()
    {
        _moveController = FindObjectOfType<MoveController>();
        if (_moveController != null)
            _moveController.OnSlidingStateChanged += OnSlidingStateChanged;

        // Oyun başında mevcut duruma göre UI'yı ayarla
        if (_moveController != null)
            OnSlidingStateChanged(_moveController.IsSliding());
    }

    private void OnDestroy()
    {
        if (_moveController != null)
            _moveController.OnSlidingStateChanged -= OnSlidingStateChanged;
    }

    // Soldaki hareket UI'ları için event fonksiyonu
    private void OnSlidingStateChanged(bool isSliding)
    {
        if (isSliding)
        {
            _playerSlidingTransform.DOAnchorPos(_slidingActivePos, _moveDuration).SetEase(_moveEase);
            _playerwalkingTransform.DOAnchorPos(_walkingPassivePos, _moveDuration).SetEase(_moveEase);
            _playerSlidingImage.sprite = _playerSlidingActiveSprite;
            _playerWalkingImage.sprite = _playerWalkingPassiveSprite;
        }
        else
        {
            _playerSlidingTransform.DOAnchorPos(_slidingPassivePos, _moveDuration).SetEase(_moveEase);
            _playerwalkingTransform.DOAnchorPos(_walkingActivePos, _moveDuration).SetEase(_moveEase);
            _playerSlidingImage.sprite = _playerSlidingPassiveSprite;
            _playerWalkingImage.sprite = _playerWalkingActiveSprite;
        }
    }

    // GOLD WHEAT BOOSTER UI
    public void ActivateGoldBoosterUI()
    {
        // Parent panel animasyonu ve sprite değişimi
        _goldBoosterTransform.DOAnchorPos(_goldBoosterActivePos, _moveDuration).SetEase(_moveEase);
        _goldBoosterImage.sprite = _goldBoosterActiveSprite;
        // Child wheat image sprite değişimi
        _goldChildImage.sprite = _goldActiveSprite;

        CancelInvoke(nameof(DeactivateGoldBoosterUI));
        Invoke(nameof(DeactivateGoldBoosterUI), _boosterActiveDuration);
    }
    private void DeactivateGoldBoosterUI()
    {
        _goldBoosterTransform.DOAnchorPos(_goldBoosterPassivePos, _moveDuration).SetEase(_moveEase);
        _goldBoosterImage.sprite = _goldBoosterPassiveSprite;
        _goldChildImage.sprite = _boosterPassiveSprite;
    }

    // HOLY WHEAT BOOSTER UI
    public void ActivateHolyBoosterUI()
    {
        _holyBoosterTransform.DOAnchorPos(_holyBoosterActivePos, _moveDuration).SetEase(_moveEase);
        _holyBoosterImage.sprite = _holyBoosterActiveSprite;
        _holyChildImage.sprite = _holyActiveSprite;

        CancelInvoke(nameof(DeactivateHolyBoosterUI));
        Invoke(nameof(DeactivateHolyBoosterUI), _boosterActiveDuration);
    }
    private void DeactivateHolyBoosterUI()
    {
        _holyBoosterTransform.DOAnchorPos(_holyBoosterPassivePos, _moveDuration).SetEase(_moveEase);
        _holyBoosterImage.sprite = _holyBoosterPassiveSprite;
        _holyChildImage.sprite = _boosterPassiveSprite;
    }

    // ROTTEN WHEAT BOOSTER UI
    public void ActivateRottenBoosterUI()
    {
        _rottenBoosterTransform.DOAnchorPos(_rottenBoosterActivePos, _moveDuration).SetEase(_moveEase);
        _rottenBoosterImage.sprite = _rottenBoosterActiveSprite;
        _wheatChildImage.sprite = _wheatActiveSprite;

        CancelInvoke(nameof(DeactivateRottenBoosterUI));
        Invoke(nameof(DeactivateRottenBoosterUI), _boosterActiveDuration);
    }
    private void DeactivateRottenBoosterUI()
    {
        _rottenBoosterTransform.DOAnchorPos(_rottenBoosterPassivePos, _moveDuration).SetEase(_moveEase);
        _rottenBoosterImage.sprite = _rottenBoosterPassiveSprite;
        _wheatChildImage.sprite = _boosterPassiveSprite;
    }
}