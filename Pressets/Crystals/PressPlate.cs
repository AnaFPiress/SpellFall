using System;
using UnityEngine;
using UnityEngine.Audio;

public class PressPlate : MonoBehaviour
{
    [SerializeField] private AudioClip DropItem;
    [SerializeField] private AudioMixer audioMixer;
    private float weight = 0f;
    private bool isActive = false;
    private float LastSound = -10000f;
    void Update()
    {
        weight = Mathf.Max(0f, weight);
        try
        {
            Debug.Log("Plate press -> " + getWeight());
        } catch(Exception e ){};
    }

    public void OnTriggerStay(Collider other)
    {
        Crystal crystal = other.GetComponent<Crystal>();
        if (crystal != null)
        {
            weight = crystal.getWeight();
            isActive = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Crystal crystal = other.GetComponent<Crystal>();
        if (crystal != null && Time.time - LastSound > 0.5f)
        {
            if (DropItem != null) GameObject.FindFirstObjectByType<SoundServer>().Play(DropItem,audioMixer.FindMatchingGroups("Effect")[0]);
            LastSound = Time.time;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Crystal crystal = other.GetComponent<Crystal>();
        if (crystal != null && weight > 0f)
        {
            weight -= crystal.getWeight();
            isActive = false;
        }
    }

    public float getWeight()
    {
        return weight;
    }
}
