using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoadingLogic : MonoBehaviour
{
    private ProjectInitializer.LevelLoader levelLoader;

    [Inject]
    public void SetDependencies(ProjectInitializer.LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    private void Start() => levelLoader.OnLoadingRequested += OnLoadingRequested;

    private void OnLoadingRequested(LevelConfig levelConfig)
    {
        StartCoroutine(StartToLoadScene(levelConfig));
    }

    private IEnumerator StartToLoadScene(LevelConfig levelConfig)
    {
        yield return LoadSceneAsync(levelConfig.logicScenePath, LoadSceneMode.Single);
        yield return LoadSceneAsync(levelConfig.artScenePath, LoadSceneMode.Additive);
        yield return LoadSceneAsync(levelConfig.audioScenePath, LoadSceneMode.Additive);
    }
    
    private IEnumerator LoadSceneAsync(string scenePath, LoadSceneMode mode = LoadSceneMode.Single)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenePath, mode);

        asyncOperation.allowSceneActivation = false;
        float progress = asyncOperation.progress;

        Debug.Log($"Loading scene {scenePath}");

        yield return new WaitUntil(() => progress < .9f);

        Debug.Log($"Scene loaded {scenePath}");
        
        yield return new WaitUntil(() => Keyboard.current.spaceKey.isPressed);

        asyncOperation.allowSceneActivation = true;
    }
}
