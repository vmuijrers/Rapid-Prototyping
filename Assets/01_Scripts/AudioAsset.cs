using UnityEngine;

[CreateAssetMenu(fileName = "AudioAsset", menuName = "Audio")]
public class AudioAsset : ScriptableObject
{
    public AudioClip[] clips;
    public float volume = 1f;
    public float pitch = 1f;
}