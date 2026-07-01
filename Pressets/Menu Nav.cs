using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    [SerializeField] private AudioClip ClickSound;
    [SerializeField] private AudioMixer audioMixer;
    private SoundServer soundServer;

    void Start()
    {
        soundServer = GameObject.FindFirstObjectByType<SoundServer>();
    }

    public void ChangeScene(String scene)
    {
        soundServer.Play(ClickSound,audioMixer.FindMatchingGroups("Effect")[0]);
        SceneManager.LoadScene(scene);
    }

    public void Quit(){ Application.Quit(); }

    public void PlayerResume(){ 
        soundServer.Play(ClickSound,audioMixer.FindMatchingGroups("Effect")[0]);
        GameObject.FindFirstObjectByType<Player>().Resume(); }

    public void OpenSettings(Canvas Settings){ 
        soundServer.Play(ClickSound,audioMixer.FindMatchingGroups("Effect")[0]);
        if (Settings != null) Settings.gameObject.SetActive(true); }
    
    public void TryAgain(){ SceneManager.LoadScene("OverWorld"); }

}
