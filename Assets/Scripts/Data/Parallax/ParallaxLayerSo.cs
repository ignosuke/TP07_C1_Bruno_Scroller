using UnityEngine;

[CreateAssetMenu(fileName = "ParallaxLayer", menuName = "Data/Parallax/Parallax Layer Data")]
public class ParallaxLayerSo : ScriptableObject
{
    [SerializeField, Range(0f, 1f)] private float followFactor = 0.8f;

    // Distinto de 0 ignora a la camara y se desplaza solo. Para el menu donde nadie se mueve
    // Negativo mueve hacia la izquierda
    [SerializeField] private float autoScrollSpeed = 0f;

    public float GetFollowFactor() => followFactor;
    public float GetAutoScrollSpeed() => autoScrollSpeed;
}