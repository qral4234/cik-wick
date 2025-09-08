using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private float _delay;
    [SerializeField] private PlayerHealthUI _playerHealthUI;


    [Header("Settings")]
    [SerializeField] private int maxEggCount = 5;

    private GameState _currentGameState;

    private int currentEggCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Singleton koruması
            return;
        }
        // DOTween kapasitesini başta ayarla
        DG.Tweening.DOTween.SetTweensCapacity(500, 50);
    }

    private void Start()
    {
        if (HealthManager.Instance != null)
            HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        if (_catController != null)
            _catController.OnCatCatched += CatController_OnCatCatched;

    }

    private void CatController_OnCatCatched()
    {
        if (_playerHealthUI != null)
            _playerHealthUI.AnimatedamageForAll();
        StartCoroutine(OnGameOver(true));
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));
    }

    void OnEnable()
    {
        ChangeGameState(GameState.Play);
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.PlayBackgroundMusic(true);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;

    }

    public void OnEggCollected()
    {
        currentEggCount++;
        if (_eggCounterUI != null)
            _eggCounterUI.SetEggCounterText(currentEggCount, maxEggCount);
        if (currentEggCount == maxEggCount)
        {
            if (_eggCounterUI != null)
                _eggCounterUI.SettEggComplated();
            ChangeGameState(GameState.GameOver);
            if (_winLoseUI != null)
                _winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver(bool isCatcatched)
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        if (_winLoseUI != null)
            _winLoseUI.OnGameLose();
        if (isCatcatched && AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(SoundType.CatSound);
        }
        
    }




    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

}
