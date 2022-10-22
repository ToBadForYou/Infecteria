using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public string eventIdentifier;
    
    public AudioSource audioSrc;
    public List<AudioClip> audioClips;

    public float playDistance;

    public void PlaySound() {
        if(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= playDistance) {
            audioSrc.clip = audioClips[Random.Range(0, audioClips.Count)];
            audioSrc.Play();
        }
    }
}
