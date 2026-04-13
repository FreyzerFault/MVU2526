using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingScreenUILogic : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private ProjectInitializer.LevelLoader levelLoader;

    [Inject]
    private void SetDependencies(ProjectInitializer.LevelLoader levelLoader)
    {
        this.levelLoader = levelLoader;
    }

    private void Update()
    {
        loadingBar.fillAmount = levelLoader.CurrentProgress;
    }
}
