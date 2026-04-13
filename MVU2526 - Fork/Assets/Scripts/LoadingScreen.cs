using UnityEngine;
using Zenject;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenPanel;
    
    private ProjectInitializer.LevelLoader levelLoader;

    [Inject]
    public void SetDependencies(ProjectInitializer.LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }
    
    private void Start()
    {
        loadingScreenPanel.SetActive(false);
        levelLoader.OnLoadingRequested += OnLoadingRequested;
    }

    private void OnLoadingRequested(LevelConfig obj) => 
        loadingScreenPanel.SetActive(true);
}
