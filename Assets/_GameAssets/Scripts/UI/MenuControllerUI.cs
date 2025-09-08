using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MaskTransitions;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel("SampleScene");
        });

        _quitButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);
            Application.Quit();
            Debug.Log("Quit Game");
        });
    }

}