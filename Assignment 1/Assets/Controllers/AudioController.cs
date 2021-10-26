using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    Dictionary<string, AudioClip> audioMap;
    public static AudioController instance; // TODO: refactor to delegates
    public GameObject soundGO;

    void OnEnable() {
        instance = this;
        LoadAudio();
    }

    private void LoadAudio() {
        audioMap = new Dictionary<string, AudioClip>();
        AudioClip[] allAudio = Resources.LoadAll<AudioClip>("/");
        foreach (AudioClip clip in allAudio) {
            Debug.Log("Loaded audio: " + clip.name);
            audioMap.Add(clip.name, clip);
        }
    }

    public void PlayMusic(string name) {
        if (name != null && audioMap.TryGetValue(name, out AudioClip clip)) {
            GameObject musicGO = soundGO.transform.Find("Music").gameObject;
            AudioSource musicAS = musicGO.GetComponent<AudioSource>();
            musicAS.clip = clip;
            musicAS.Play();
        } else {
            StopBackgroundMusic();
        }
    }

    public void PlaySFX(string name) {
        if (name != null && audioMap.TryGetValue(name, out AudioClip clip)) {
            GameObject sfxGO = soundGO.transform.Find("SFX").gameObject;
            AudioSource sfxAS = sfxGO.GetComponent<AudioSource>();
            sfxAS.clip = clip;
            sfxAS.Play();
        }
    }

    public void PlayLoopingSFX(string name) {
        if (name != null && audioMap.TryGetValue(name, out AudioClip clip)) {
            GameObject sfxGO = soundGO.transform.Find("LoopSFX").gameObject;
            AudioSource sfxAS = sfxGO.GetComponent<AudioSource>();
            sfxAS.clip = clip;
            sfxAS.Play();
        }
    }

    public void StopBackgroundMusic() {
        GameObject musicGO = soundGO.transform.Find("Music").gameObject;
        AudioSource musicAS = musicGO.GetComponent<AudioSource>();
        musicAS.Stop();
    }

    public void StopSFX() {
        GameObject sfxGO = soundGO.transform.Find("SFX").gameObject;
        AudioSource sfxAS = sfxGO.GetComponent<AudioSource>();
        sfxAS.Stop();
    }

    public void UpdateVolumeMusic(float volume) {
        GameObject musicGO = soundGO.transform.Find("Music").gameObject;
        AudioSource musicAS = musicGO.GetComponent<AudioSource>();
        musicAS.volume = volume;
    }

    public void UpdateVolumeSFX(float volume) {
        GameObject sfxGO = soundGO.transform.Find("SFX").gameObject;
        AudioSource sfxAS = sfxGO.GetComponent<AudioSource>();
        sfxAS.volume = volume;
        GameObject loopsfxGO = soundGO.transform.Find("LoopSFX").gameObject;
        AudioSource loopsfxAS = loopsfxGO.GetComponent<AudioSource>();
        loopsfxAS.volume = volume;
    }
}