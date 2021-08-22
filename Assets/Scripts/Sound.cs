using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1;
    [Range(0.1f, 3f)]
    public float Pitch = 1;

    public bool Loop;

    public enum SoundType
    {
        SFX,
        Music
    }
    public SoundType Type = SoundType.SFX;

    [HideInInspector]
    public AudioSource Source;
}