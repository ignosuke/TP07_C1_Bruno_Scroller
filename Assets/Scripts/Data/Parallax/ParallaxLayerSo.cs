using UnityEngine;

[CreateAssetMenu(fileName = "ParallaxLayer", menuName = "Data/Parallax/Parallax Layer Data")]
public class ParallaxLayerSo : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] private float followFactor = 0.8f;

    public float GetFollowFactor() => followFactor;
}