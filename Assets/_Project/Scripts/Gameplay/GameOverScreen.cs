using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : UiPanel
{
    [SerializeField] private Button _buttonRestart;

    private void Awake()
    {
        GlobalEvents.EventIvanIsDead.AddListener(OnIvanDeath);
        _buttonRestart.onClick.AddListener(RestartScene);
    }

    private void OnDestroy()
    {
        GlobalEvents.EventIvanIsDead.RemoveListener(OnIvanDeath);
        _buttonRestart.onClick.RemoveListener(RestartScene);
    }

    private void OnIvanDeath()
    {
        Activate(true, 0.3f);
    }

    private async void RestartScene()
    {
        await SceneTransition.instance.ReloadCurrentScene();
    }
}
