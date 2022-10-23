using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer mixer;

    // audio source pooling
    public GameObject audioPlayerPrefab;
    Queue<AudioPlayer> inactiveAudioPlayers; //audio players ready for reuse
    IList<AudioPlayer> audioPlayers; //all audio players
    Dictionary<int, AudioPlayer> activeAudioPlayers; //all active audio players

    // Music
    AudioSource musicSource;
    [SerializeField] AudioMixerGroup musicMixerGroup;
    AudioAsset currMusic;

    private const string musicVolumeName = "MusicVolume";
    private const string sfxVolumeName = "SfxVolume";
    
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        inactiveAudioPlayers = new Queue<AudioPlayer>();
        audioPlayers = new List<AudioPlayer>();
        activeAudioPlayers = new Dictionary<int, AudioPlayer>();
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicMixerGroup;

        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChange);
    }

    private void Start()
    {
        var musicVol = PlayerPrefs.GetFloat(musicVolumeName, 1f);
        var sfxVol = PlayerPrefs.GetFloat(sfxVolumeName, 1f);
        mixer.SetFloat(musicVolumeName, Mathf.Log10(musicVol) * 20f);
        mixer.SetFloat(sfxVolumeName, Mathf.Log10(sfxVol) * 20f);
        musicVolumeSlider.value = musicVol;
        sfxVolumeSlider.value = sfxVol;
    }

    /// <summary>
    /// Play 2D audio
    /// </summary>
    /// <param name="audio"></param>
    /// <returns></returns>
    public int Play(AudioAsset audio)
    {
        if (audio.clip == null && audio.randomClips.Count != 0)
        {
            return PlayRandom(audio);
        }
        if (audio == null) return -1;

        AudioPlayer audioPlayer = GetAudioPlayer();
        if (audioPlayer == null) return -1;
        
        audioPlayer.Play(audio);
        activeAudioPlayers[audioPlayer.ID] = audioPlayer;
        return audioPlayer.ID;
    }

    public int PlayRandom(AudioAsset audio)
    {
        if (audio == null) return -1;

        AudioPlayer audioPlayer = GetAudioPlayer();
        if (audioPlayer == null) return -1;

        audioPlayer.PlayRandom(audio);
        activeAudioPlayers[audioPlayer.ID] = audioPlayer;
        return audioPlayer.ID;
    }
    
    public int PlayOnBeat(AudioAsset audio, float bpm, float offset)
    {
        if (audio == null) return -1;

        AudioPlayer audioPlayer = GetAudioPlayer();
        if (audioPlayer == null) return -1;

        var timePerBeat = 60 / bpm;
        var timeToWait = timePerBeat - offset % timePerBeat;
        StartCoroutine(PlayAfterTime(audioPlayer, audio, timeToWait));
        activeAudioPlayers[audioPlayer.ID] = audioPlayer;
        return audioPlayer.ID;
    }

    private IEnumerator PlayAfterTime(AudioPlayer audioPlayer, AudioAsset audio, float time)
    {
        audioPlayer.SetupPlay(audio);
        yield return new WaitForSeconds(time);
        audioPlayer.JustPlay();
    }
    
    private IEnumerator PlayMusicAfterTime(int lockingChannelId, AudioAsset audio, float time)
    {
        musicSource.time = 0.0f;
        musicSource.clip = audio.clip;
        musicSource.volume = audio.volume;
        musicSource.pitch = audio.pitch;
        musicSource.loop = audio.loop;
        if (time > 1)
        {
            yield return new WaitForSeconds(time - 1);
        }
        while (activeAudioPlayers.ContainsKey(lockingChannelId) && activeAudioPlayers[lockingChannelId].Source.isPlaying)
        {
            yield return null;
        }
        musicSource.Play();
        currMusic = audio;
    }

    /// <summary>
    /// Play sound that ignores listener pause state
    /// </summary>
    /// <param name="audio"></param>
    /// <returns></returns>
    public int PlayUI(AudioAsset audio)
    {
        if (audio == null) return -1;

        AudioPlayer audioPlayer = GetAudioPlayer();
        if (audioPlayer == null) return -1;

        audioPlayer.Source.ignoreListenerPause = true;
        audioPlayer.Play(audio);
        activeAudioPlayers[audioPlayer.ID] = audioPlayer;
        return audioPlayer.ID;
    }

    // get audio source from pool or create new one
    private AudioPlayer GetAudioPlayer()
    {
        AudioPlayer audioPlayer;
        if (inactiveAudioPlayers.Count == 0)
        {
            audioPlayer = new AudioPlayer(Instantiate(audioPlayerPrefab, transform).GetComponent<AudioSource>(), this);
        }
        else
        {
            audioPlayer = inactiveAudioPlayers.Dequeue();
        }
        return audioPlayer;
    }

    public void AddAudioPlayer(AudioPlayer player)
    {
        audioPlayers.Add(player);
    }

    public void EnqueueAudioPlayer(AudioPlayer audioPlayer)
    {
        //add to audio source pool
        inactiveAudioPlayers.Enqueue(audioPlayer);
        audioPlayer.Source.ignoreListenerPause = false;

        //remove from active players
        activeAudioPlayers.Remove(audioPlayer.ID);
    }

    public void Stop(int ID)
    {
        if (!activeAudioPlayers.ContainsKey(ID)) return;
        AudioPlayer player = activeAudioPlayers[ID];
        player?.Stop();
    }

    public void Pause(int ID)
    {
        if (!activeAudioPlayers.ContainsKey(ID)) return;
        AudioPlayer player = activeAudioPlayers[ID];
        player?.Pause();
    }

    public bool IsPlaying(int ID)
    {
        if (!activeAudioPlayers.ContainsKey(ID)) return false;
        return true;
    }
    
    public void PlayMusic(AudioAsset startAudio, AudioAsset loopAudio)
    {
        int startChannel = Play(startAudio);
        StartCoroutine(PlayMusicAfterTime(startChannel, loopAudio, startAudio.clip.length));
    }
    
    public void PlayMusic(AudioAsset audio)
    {
        musicSource.Stop();
        musicSource.time = 0.0f;
        musicSource.clip = audio.clip;
        musicSource.volume = audio.volume;
        musicSource.pitch = audio.pitch;
        musicSource.loop = audio.loop;
        musicSource.Play();
        currMusic = audio;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void SetVolume(string mixerChannel, float value) {
        mixer.SetFloat(mixerChannel, Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat(mixerChannel, value);
    }
    
    public void StopGameSound() {
        AudioListener.pause = true;
    }
    public void ResumeGameSound() {
        AudioListener.pause = false;
    }

    private void Update()
    {
        if (AudioListener.pause) return;
        foreach (var player in audioPlayers)
        {
            player.Update();
        }
    }
    public void OnMusicVolumeChange(float value)
    {
        mixer.SetFloat(musicVolumeName, Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat(musicVolumeName, value);
    }
    
    public void OnSfxVolumeChange(float value)
    {
        mixer.SetFloat(sfxVolumeName, Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat(sfxVolumeName, value);
    }
}
