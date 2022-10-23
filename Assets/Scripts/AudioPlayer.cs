using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// helps manage pooling of audio sources
public class AudioPlayer
{
    private AudioManager audioManager;
    private AudioSource source;
    public AudioSource Source => source;

    private int currID = -1;
    public int ID => currID;

    private float clipTime;
    private bool playing;

    public AudioPlayer(AudioSource source, AudioManager audioManager)
    {
        this.source = source;
        this.audioManager = audioManager;
        this.playing = false;
        this.audioManager.AddAudioPlayer(this);
    }

    public void Play(AudioAsset audio)
    {
        SetupPlay(audio);
        source.Play();
    }

    public void PlayRandom(AudioAsset audio)
    {
        SetupRandomPlay(audio);
        source.Play();
    }

    public void JustPlay()
    {
        source.Play();
    }

    public void SetupPlay(AudioAsset audio)
    {
        if (audio == null || audio.clip == null) return;
        source.gameObject.transform.parent = audioManager.gameObject.transform;
        source.gameObject.transform.position = audioManager.gameObject.transform.position;
        SetupAudio(audio);
        source.spatialBlend = 0f;
        
    }
    
    public void SetupRandomPlay(AudioAsset audio)
    {
        if (audio == null || audio.randomClips.Count == 0) return;
        source.gameObject.transform.parent = audioManager.gameObject.transform;
        source.gameObject.transform.position = audioManager.gameObject.transform.position;
        SetupRandomAudio(audio);
        source.spatialBlend = 0f;
        
    }

    private void SetupAudio(AudioAsset audio)
    {
        source.time = 0.0f;
        source.clip = audio.clip;
        source.outputAudioMixerGroup = audio.mixer;
        source.loop = audio.loop;
        source.volume = audio.volume;
        source.pitch = audio.pitch;
        clipTime = source.clip.length;
        playing = true;
        currID = Random.Range(0, int.MaxValue);
    }
    
    private void SetupRandomAudio(AudioAsset audio)
    {
        source.time = 0.0f;
        var index = Random.Range(0, audio.randomClips.Count);
        source.clip = audio.randomClips[index];
        source.outputAudioMixerGroup = audio.mixer;
        source.loop = audio.loop;
        source.volume = audio.volume;
        source.pitch = audio.pitch;
        clipTime = source.clip.length;
        playing = true;
        currID = Random.Range(0, int.MaxValue);
    }

    public void Stop()
    {
        source.Stop();
        audioManager.EnqueueAudioPlayer(this);
        currID = -1;
        playing = false;
    }

    public void Pause()
    {
        source.Pause();
        playing = false;
    }

    public void Update()
    {
        if (!playing || source.loop) return;
        clipTime -= Time.deltaTime;
        if (clipTime <= 0f)
        {
            audioManager.EnqueueAudioPlayer(this);
            currID = -1;
            playing = false;
        }
    }
}
