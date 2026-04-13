using UnityEngine;
using Zenject;

public class ProjectInitializer : MonoInstaller
{
    public override void InstallBindings()
    {
        // Preparamos el AudioSystem
        Container.Bind<AudioSystem>().AsSingle();
        Container.Bind<LevelLoader>().AsSingle();
        
        // O puedes crearlo a partir de una instancia ya creada
        // Container.Bind<AudioSystem>().FromInstance(new AudioSystem());
    }
    
    public class AudioSystem 
    {
        public void EmitSound(string id) 
        {
            Debug.Log($"Sound Emited {id}");
        }
    }

    public class LevelLoader
    {
        public void LoadLevel(object level)
        {
            
        }
    }
}
