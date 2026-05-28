using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

[CreateAssetMenu(fileName = "Stable Scenes", menuName = "Tests/Stable Scenes GUIDs")]
public class StableScenes : ScriptableObject
{
    public List<SceneData> stableScenesGuids = new()
    {
        new SceneData { guid = "966fb10d10f8d7e4d986660edc1516c0" },
        new SceneData { guid = "4c078d33c9929da4984c17d4acfd3c46" },
        new SceneData { guid = "8f22165a8a02cd1409e7f62c102f2c57" },
    };
}

[CustomEditor(typeof(StableScenes))]
public class StableScenesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reload Scripts"))
        {
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}
