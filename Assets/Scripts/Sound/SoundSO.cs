using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSO", menuName = "Scriptable Objects/SoundSO")]
public class SoundSO : ScriptableObject
{
    public Sounds[] sounds;
}
[Serializable]
public struct Sounds
{
    public SoundType type;
    public AudioClip clip;
}
