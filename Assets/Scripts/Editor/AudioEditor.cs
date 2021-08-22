using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AudioManager sound = (AudioManager)target;

        for (int i = 0; i < sound.Sounds.Length; i++)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play \"" + sound.Sounds[i].Name + '"'))
            {
                sound.Play(sound.Sounds[i].Name);
            }
            if (GUILayout.Button("Stop \"" + sound.Sounds[i].Name + '"'))
            {
                sound.Stop(sound.Sounds[i].Name);
            }
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Stop All"))
        {
            sound.StopAll();
        }
    }
}

[CustomEditor(typeof(AudioSource))]
public class SourceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AudioSource source = (AudioSource)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Play"))
        {
            source.Play();
        }
        if (GUILayout.Button("Stop"))
        {
            source.Stop();
        }
        GUILayout.EndHorizontal();
    }
}
