using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : UiPanel
{
    [Header("References to objects")]
    [SerializeField] 
    private Button buttonPause;
    [SerializeField] 
    private Button buttonContinue;

    [SerializeField] 
    private Button buttonSettings;

    [SerializeField] 
    private Button buttonToMainMenu;

    [SerializeField] 
    private Button buttonQuit;

    private void Awake()
    {
        buttonPause.onClick.AddListener(() => Activate(true));
        buttonContinue.onClick.AddListener(() => Activate(false));
        buttonToMainMenu.onClick.AddListener(LoadMainMenu);
        buttonSettings.onClick.AddListener(ShowSettings);

#if !UNITY_EDITOR && (UNITY_WEBGL || UNITY_IOS || UNITY_ANDROID)
        buttonQuit.gameObject.SetActive(false);
#else
        buttonQuit.onClick.AddListener(Quit);
#endif
    }

    private void OnDestroy()
    {
        buttonPause.onClick.RemoveAllListeners();
        buttonContinue.onClick.RemoveAllListeners();
        buttonToMainMenu.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonQuit.onClick.RemoveAllListeners();
    }

    private new void Activate(bool active)
    {
        Time.timeScale = active ? 1.0f : 0.0f;
        buttonPause.gameObject.SetActive(!active);
        base.Activate(active);
    }

    private void LoadMainMenu()
    {
        //SceneManager.LoadScene(0);
        SceneTransition.instance.SwitchToMainMenu();
    }

    private void ShowSettings()
    {
        if (Settings.instance != null)
        {
            Settings.instance.Activate(true);
        }
        else
        {
            Debug.Log($"MainMenu: ShowSettings: there is no Settings");
        }
    }

    private void Quit()
    {
        Application.Quit();
    }
}
