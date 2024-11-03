using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private TextMeshProUGUI textVersion;

    private SceneTransition _sceneTransition;

    private void Awake()
    {
        _sceneTransition = FindObjectOfType<SceneTransition>();
        if (_sceneTransition == null)
        {
            Debug.LogError($"MainMenu: Awake: _sceneTransition is null");
            return;
        }

        buttonStart.onClick.AddListener(_sceneTransition.SwitchToNextScene);
        buttonSettings.onClick.AddListener(ShowSettings);

#if !UNITY_EDITOR && (UNITY_WEBGL || UNITY_IOS || UNITY_ANDROID)
        buttonQuit.gameObject.SetActive(false);
#else
        buttonQuit.onClick.AddListener(Quit);
#endif

        textVersion.text = "version " + Application.version;
    }

    private void OnDestroy()
    {
        buttonStart.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();
        buttonQuit.onClick.RemoveAllListeners();
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
