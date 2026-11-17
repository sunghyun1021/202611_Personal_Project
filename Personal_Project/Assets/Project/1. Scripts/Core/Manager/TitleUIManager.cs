using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : SingletonBehaviour<TitleUIManager>
{
    [SerializeField] private GameObject _settingUI;
    [SerializeField] private GameObject _creditUI;
    [SerializeField] private Button _gameStartBtn;
    [SerializeField] private Button _onSettingsBtn;
    [SerializeField] private Button _offSettingsBtn;
    [SerializeField] private Button _onCreditBtn;
    [SerializeField] private Button _offCreditBtn;
    [SerializeField] private Button _exitBtn;
    
    // ---------- Event Method ----------
    
    private void Awake() => SetSingleton();

    private void OnEnable() => BindButtonEvent();
    
    private void OnDisable() => UnBindButtonEvent();

    // ----------------------------------

    private void BindButtonEvent()
    {
        _gameStartBtn.onClick.AddListener(LoadGameScene);
        _onSettingsBtn.onClick.AddListener(ActiveSettingsUI);
        _offSettingsBtn.onClick.AddListener(DeactiveSettingsUI);
        _onCreditBtn.onClick.AddListener(ActiveCreditUI);
        _offCreditBtn.onClick.AddListener(DeactiveCreditUI);
        _exitBtn.onClick.AddListener(ExitGame);
    }

    private void UnBindButtonEvent()
    {
        _gameStartBtn.onClick.RemoveListener(LoadGameScene);
        _onSettingsBtn.onClick.RemoveListener(ActiveSettingsUI);
        _offSettingsBtn.onClick.RemoveListener(DeactiveSettingsUI);
        _onCreditBtn.onClick.RemoveListener(ActiveCreditUI);
        _offCreditBtn.onClick.RemoveListener(DeactiveCreditUI);
        _exitBtn.onClick.RemoveListener(ExitGame);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void ActiveSettingsUI()
    {
        _settingUI.SetActive(true);
    }

    private void DeactiveSettingsUI()
    {
        _settingUI.SetActive(false);
    }

    private void ActiveCreditUI()
    {
        _creditUI.SetActive(true);
    }

    private void DeactiveCreditUI()
    {
        _creditUI.SetActive(false);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
