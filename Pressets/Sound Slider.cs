using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SoundType
{
    Master,
    Music,
    Effect
}


[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private SoundType type;
    [SerializeField] private AudioMixer mixer; 

    void Awake(){
        _slider = GetComponent<Slider>();
        if (mixer == null){
            Destroy(this.gameObject);
            return;
        }
        float value = PlayerPrefs.GetFloat("Audio_" + getMixerGroup(),-999999);
        if (value != -999999){
            mixer.SetFloat(getMixerGroup(),value);
        }
    }

    void OnDisable()
    {
        float value = PlayerPrefs.GetFloat("Audio_" + getMixerGroup(),-999999);
        Debug.Log("Audio_" + getMixerGroup() + " teste audio");
        if (value != -999999){
            mixer.SetFloat(getMixerGroup(),value);
        }
    }

    void Start()
    {
        float value = PlayerPrefs.GetFloat("Audio_" + getMixerGroup(),0);
        mixer.SetFloat(getMixerGroup(),value);
        _slider.value = value;
    }

    private string getMixerGroup()
    {
        string soundType = "";
        if (type == SoundType.Master) soundType = "Master";
        else if (type == SoundType.Music) soundType = "Music";
        else if (type == SoundType.Effect) soundType = "Effect";
        return soundType;
    }

    void Update()
    {
        PlayerPrefs.SetFloat("Audio_" + getMixerGroup(),_slider.value);
        bool success = mixer.SetFloat(getMixerGroup(),_slider.value);
        Debug.Log("Audio change with success -> " + success);
    }
}
