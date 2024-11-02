using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Cysharp.Threading.Tasks;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance { get; private set;} = null;

    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private float timeForActivateLoadingScreen = 0.5f;
    private AsyncOperation loadingSceneOperation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public async void SwitchToNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        await SwitchToSceneAsync(sceneIndex);
    }

    public async UniTask ReloadCurrentScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        await SwitchToSceneAsync(sceneIndex);
    }

    public async void SwitchToMainMenu()
    {
        await SwitchToSceneAsync((int)SceneNames.MainMenu);
    }

    private async UniTask SwitchToSceneAsync(int sceneIndex)
    {
        GlobalEvents.EventStartSceneLoading?.Invoke(sceneIndex);

        // Activate Loading Screen
        loadingScreen.UpdateProgress(0f, true);
        loadingScreen.Activate(true, timeForActivateLoadingScreen);

        // Load empty Boot scene first for freeing RAM
        await SceneManager.LoadSceneAsync(SceneNames.Boot.ToString());

        // Load target scene
        loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
        //loadingSceneOperation.allowSceneActivation = false; // can be useful in some cases

        // Update progress in Loading Screen
        while (!loadingSceneOperation.isDone)
        {
            loadingScreen.UpdateProgress(loadingSceneOperation.progress);
            await UniTask.NextFrame();
        }

        // Deactivate Loading Screen
        loadingScreen.UpdateProgress(1f, true);
        loadingScreen.Activate(false, timeForActivateLoadingScreen);

        GlobalEvents.EventEndSceneLoading?.Invoke(sceneIndex);
    }
}
