using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoadingLogic : MonoBehaviour
{
    private ProjectInitializer.LevelLoader levelLoader;
 
    private string LOADING_SCREEN_PATH = "Loading Screen";
    private List<AsyncOperation> loadingOperations = new();

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
        AsyncOperation loadingScreenOperation = SceneManager.LoadSceneAsync(LOADING_SCREEN_PATH, LoadSceneMode.Single);
        yield return new WaitUntil(() => loadingScreenOperation.isDone);
        
        levelLoader.CurrentProgress = 0f;
        yield return LoadSceneAsync(levelConfig.logicScenePath, LoadSceneMode.Additive);
        levelLoader.CurrentProgress = .33f;
        yield return LoadSceneAsync(levelConfig.artScenePath, LoadSceneMode.Additive);
        levelLoader.CurrentProgress = .66f;
        yield return LoadSceneAsync(levelConfig.audioScenePath, LoadSceneMode.Additive);
        levelLoader.CurrentProgress = 1f;

        foreach (AsyncOperation loadingOperation in loadingOperations) 
            loadingOperation.allowSceneActivation = true;
        
        yield return new WaitUntil(() => loadingOperations.All(ao => ao.isDone));

        AsyncOperation unloadingScene = SceneManager.UnloadSceneAsync(LOADING_SCREEN_PATH);
    }
    
    private IEnumerator LoadSceneAsync(string scenePath, LoadSceneMode mode = LoadSceneMode.Single)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenePath, mode);
        
        // Lo Prepara en la lista para cuando todos carguen bien poder cargarlos de golpe 
        loadingOperations.Add(asyncOperation);
        
        // No le permite cargar cuando se complete hasta que yo diga
        asyncOperation.allowSceneActivation = false;

        // Mock Load Time
        yield return new WaitForSeconds(1);
        
        yield return new WaitUntil(() => asyncOperation.progress >= .9f);
    }
}
