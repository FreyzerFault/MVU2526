using System;
using UnityEditor;

[Serializable]
public class SceneData
{
    public string guid;
    public string Path => AssetDatabase.GUIDToAssetPath(guid);
    public override string ToString() => Path;
            
    public static implicit operator string(SceneData data) => data.guid;
}
