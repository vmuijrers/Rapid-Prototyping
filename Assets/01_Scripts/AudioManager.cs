using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void PlayAudioRandom(AudioAsset asset)
    {
        GameObject obj = new GameObject(asset.name);
        var aSource = obj.AddComponent<AudioSource>();
        aSource.clip = asset.clips.GetRandomItem();
        aSource.volume = asset.volume;
        aSource.pitch = asset.pitch;
        aSource.Play();
        Destroy(obj, aSource.clip.length + 0.01f);
    }
}
