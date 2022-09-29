using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : DetectorUnit
{
    public GameObject reportTo;
    public GameObject scoutObject;
    public GameObject antibodyObject;
    public UnitSquad unitSquad;
    public bool isAlerted;
    public int maxAntibodies = 3;
    UnitSpawner unitSpawner;
    
    public AudioSource audioSrc;
    public AudioClip soundEffect;
    
    new void Start(){
        base.Start();
        SetUnitStats(50, 50, 0, 1, 1.0f, 0.2f, false);
        reportTo = GameObject.Find("Heart");
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
    }

    public override void Attack(){
        if (!isAlerted){
            isAlerted = true;
            Scout newScout = unitSpawner.SpawnScout(transform.position, reportTo);
            newScout.SetAlerted(gameObject);
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, Random.Range(1, maxAntibodies));
            unitSquad.AddUnits(antibodies);

            audioSrc = GameObject.Find("Sound Effect Player").GetComponent<AudioSource>();
            audioSrc.clip = soundEffect;
            audioSrc.Play();
        }
    }

    public override void StopAttack(){
        
    }
}
