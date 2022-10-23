using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioAsset", menuName = "AudioAsset", order = 0)]
public class AudioAsset : ScriptableObject
{
    public AudioClip clip;
    public List<AudioClip> randomClips;
    public AudioMixerGroup mixer;
    public float clipLength => clip ? clip.length : 0f;
    public bool loop;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
}