using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundServer : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Sound Server: is ready .🎶");
        name = "Sound Server";
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(AudioClean());
    }

    public void Play(AudioClip clip, AudioMixerGroup group)
    {
        if (clip == null) return;
        AudioSource audio = new GameObject().AddComponent<AudioSource>();
        audio.transform.SetParent(transform);
        audio.clip = clip;
        audio.outputAudioMixerGroup = group;
        audio.volume = 1.0f;
        audio.Play();
    }

    private IEnumerator AudioClean()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            foreach(Transform child in transform)
            {
                AudioSource audio = child.GetComponent<AudioSource>();
                if (!audio) continue;
                if (!audio.isPlaying) Destroy(audio.gameObject);
            }
        }
    }
}
