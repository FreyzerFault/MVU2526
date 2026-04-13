using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private ProjectInitializer.AudioSystem audioSystem;
    private ProjectInitializer.LevelLoader levelLoader;
    
    [Inject]
    public void SetDependencies(
        ProjectInitializer.AudioSystem audioSystem,
        ProjectInitializer.LevelLoader levelLoader) {
        this.audioSystem = audioSystem;
        this.levelLoader = levelLoader;
    }
    
    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
            audioSystem.EmitSound("Jump Sound");
        
        if (Keyboard.current.f1Key.wasPressedThisFrame) 
        {
            levelLoader.LoadLevel()
        }
    }
}
