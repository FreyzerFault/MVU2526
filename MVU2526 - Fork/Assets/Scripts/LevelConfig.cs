using UnityEngine;

[CreateAssetMenu(menuName = "Level Loading/Level Config")]
public class LevelConfig : ScriptableObject
{
    public string logicScenePath;
    public string audioScenePath;
    public string artScenePath;
}
