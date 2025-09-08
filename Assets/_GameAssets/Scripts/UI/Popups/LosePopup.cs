using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using MaskTransitions;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        if (_timerUI == null || _timerText == null || _tryAgainButton == null || _mainMenuButton == null)
        {
            Debug.LogWarning("LosePopup: One or more UI references are missing!");
            return;
        }
        if (BackgroundMusic.Instance != null)
            BackgroundMusic.Instance.PlayBackgroundMusic(false);
        if (AudioManager.Instance != null)
            AudioManager.Instance.Play(SoundType.LoseSound);
        _timerText.text = _timerUI.GetFinalTime();
        _tryAgainButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        _mainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel("MenuScene");
            if (AudioManager.Instance != null)
                AudioManager.Instance.Play(SoundType.TransitionSound);
        });
    }

    private void OnTryAgainButtonClicked()
    {

        TransitionManager.Instance.LoadLevel("SampleScene");
        AudioManager.Instance.Play(SoundType.TransitionSound);
    }
}
