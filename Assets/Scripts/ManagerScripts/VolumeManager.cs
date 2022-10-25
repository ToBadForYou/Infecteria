using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeManager : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    float currentVolume = 1;
    public List<AudioSource> sources = new List<AudioSource>();

    public void AddSource(AudioSource newSrc){
        newSrc.volume = currentVolume;
        sources.Add(newSrc);
    }

    public void changeVolume(System.Single sliderValue){
        foreach(AudioSource audioSrc in sources){
            if(audioSrc != null){
                audioSrc.volume = sliderValue;
            }
        }
        currentVolume = sliderValue;
        amountText.text = Mathf.Round(sliderValue*100).ToString();
    }        
}
