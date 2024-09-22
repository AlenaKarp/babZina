//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelSettings), menuName = "Configs" + "/" + nameof(LevelSettings))]
public class LevelSettings : ScriptableObject
{
    [Serializable]
    public struct LevelResult
    {
        public int starCount;
        public int successTricks;
        public int failedTricks;
    }

    public string serializedKey;
    public int maxTricksCount;
    public int skippableTricks;
    public LevelResult[] levelResults;
    public int levelIndex;
}
