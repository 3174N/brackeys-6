using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Runtime.InteropServices;

public class AudioManager : MonoBehaviour
{
    // To use from another script use:
    // AudioManager.Play("theme");

    public static AudioManager Instance;

    public Sound[] Sounds;

    // Start is called before the first frame update
    void Awake()
    {
        // Make sure this is the only instance
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {
            if (s.Source == null)
            {
                AddSource(s);
            }
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    /// <summary>
    /// Plays the sound with the coresponding name.
    /// </summary>
    /// <param name="name">Sound name.</param>
    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }
        if (s.Source != null)
            s.Source.Play();
        else
        {
            AddSource(s);

            s.Source.Play();
        }
    }

    /// <summary>
    /// Stops the sound with the coresponding name.
    /// </summary>
    /// <param name="name">Sound name.</param>
    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }
        if (s.Source != null)
            s.Source.Stop();
        else
        {
            AddSource(s);

            s.Source.Stop();
        }
    }

    /// <summary>
    /// Stops all sounds that are currently playing.
    /// </summary>
    public void StopAll()
    {
        foreach (Sound sound in Sounds)
        {
            Stop(sound.Name);
        }
    }

    /// <summary>
    /// Adds an audio source to the gameObject.
    /// </summary>
    /// <param name="s">Sound.</param>

    void AddSource(Sound s)
    {
        s.Source = gameObject.AddComponent<AudioSource>();
        s.Source.clip = s.Clip;

        s.Source.volume = s.Volume;
        s.Source.pitch = s.Pitch;
        s.Source.loop = s.Loop;
    }
}