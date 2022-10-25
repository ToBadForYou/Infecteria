using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public string eventIdentifier;
    
    public GameObject audioSrcPrefab;

    public AudioSource audioSrc;
    public List<AudioClip> audioClips;

    public float playDistance;

    public bool isGlobal;
    public bool hasAudioSrc;

    public int buildingIndex;

    VolumeManager volumeManager;

    void Start(){
        volumeManager = GameObject.Find("VolumeManager").GetComponent<VolumeManager>();
    }

    public void CreateAudioSrc() {
        if(!hasAudioSrc) {
            if(!audioSrc) {
                GameObject audioSrcObj = Instantiate(audioSrcPrefab);
                audioSrcObj.transform.parent = transform;
                audioSrc = audioSrcObj.GetComponent<AudioSource>();
                if(volumeManager == null){
                    volumeManager = GameObject.Find("VolumeManager").GetComponent<VolumeManager>();
                }
                volumeManager.AddSource(audioSrc);
            }
        }
    }

    public void PlaySound() {
        if(isGlobal) {
            audioSrc.clip = audioClips[Random.Range(0, audioClips.Count)];
            if(eventIdentifier == "Build") {
                if(GameObject.Find("GameManager").GetComponent<GameManager>().CanAffordSugar(GameObject.Find("FactoryOverview").GetComponent<FactoryManager>().availableStructures[buildingIndex].cost))
                    audioSrc.Play();
            }
            else
                audioSrc.Play();
        }
        else {
            if(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= playDistance) {
                audioSrc.clip = audioClips[Random.Range(0, audioClips.Count)];
                audioSrc.Play();
            }
        }
    }

    public void StopSound() {
        audioSrc.Stop();
    }
}
