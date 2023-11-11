using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    List<AudioSource> currentAudioSources = new List<AudioSource>();
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;

    // Start is called before the first frame update
    void Start()
    {
        currentAudioSources.Add(GetComponent<AudioSource>());
    }

    public void PlayOneShot(AudioClip clip, bool isMusic)
    {
        foreach (AudioSource source in currentAudioSources)
        {
            if (source.isPlaying)
                continue;

            source.PlayOneShot(clip);
            source.outputAudioMixerGroup = isMusic ? musicGroup : sfxGroup;
            return;
        }

        AudioSource temp = gameObject.AddComponent<AudioSource>();
        currentAudioSources.Add(temp);
        temp.PlayOneShot(clip);
        temp.outputAudioMixerGroup = isMusic ? musicGroup : sfxGroup;
    }
}