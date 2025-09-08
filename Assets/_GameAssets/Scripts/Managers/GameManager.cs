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

    }

    private void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;

    }

    private void CatController_OnCatCatched()
    {
        _playerHealthUI.AnimatedamageForAll();
        StartCoroutine(OnGameOver());
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver());
    }

    void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log($"Game State changed to: {gameState}");
    }

    public void OnEggCollected()
    {
        currentEggCount++;
        _eggCounterUI.SetEggCounterText(currentEggCount, maxEggCount);



        if (currentEggCount == maxEggCount)
        {

            _eggCounterUI.SettEggComplated();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }


    }

    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }




    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

}
