//this empty line for UTF-8 BOM header
using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AngryScaleConfig), menuName = "Configs" + "/" + nameof(AngryScaleConfig))]
public class AngryScaleConfig : ScriptableObject
{
    [Serializable]
    public struct TrickScore
    {
        public PlayerPhysics.TricksType type;
        public int score;
    }

    public int startValue;
    public int decreasePointInSecond;
    public TrickScore[] tricksScore;
    public int trickFailScore;
}
