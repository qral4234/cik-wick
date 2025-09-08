using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MaskTransitions;


public class SettingsUI : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private GameObject _settingsPopupObject;
 



    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;




    private void Awake()
    {
        _settingsPopupObject.SetActive(true);

        _settingsPopupObject.transform.localScale = Vector3.zero;

        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);

        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        
        _mainMenuButton.onClick.AddListener(() =>
        {

            TransitionManager.Instance.LoadLevel("MenuScene");
        });

    }

    private void OnSettingsButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        _settingsPopupObject.SetActive(true);


        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    private void OnResumeButtonClicked()
    {
        GameManager.Instance.ChangeGameState(GameState.Resume);

        _settingsPopupObject.transform.DOScale(0f, _animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            _settingsPopupObject.SetActive(false);
        });
    }


}
