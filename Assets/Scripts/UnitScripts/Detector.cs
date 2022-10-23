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
    UnitSpawner unitSpawner;
    public UnitProducer unitProducer;
    
    public SoundManager detectedSoundManager;
    
    new void Start(){
        base.Start();
        SetUnitStats(50, 50, 0, 1, 1.0f, 0.2f, false);
        reportTo = GameObject.Find("Heart");
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        unitProducer.AddProduction(UnitType.ANTIBODY, new UnitProductionData(3, 3, 20));
        unitProducer.AddProduction(UnitType.SCOUT, new UnitProductionData(1, 1, 60));
    }

    public override void Attack(){
        if (!isAlerted){
            isAlerted = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().timesDetectedByDetector++;

            int availableScout = unitProducer.WithdrawAmount(UnitType.SCOUT, 1);
            if(availableScout > 0){            
                Scout newScout = unitSpawner.SpawnScout(transform.position, reportTo);
                newScout.SetAlerted(gameObject);
            }

            int currentAntibodies = unitProducer.WithdrawAll(UnitType.ANTIBODY);
            if(currentAntibodies > 0){
                List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
                unitSquad.AddUnits(antibodies);
                unitSquad.MoveTo(transform.position, false);
                foreach (Unit antibody in antibodies){
                    antibody.producer = unitProducer;
                    antibody.GiveTask(new ReturnTask(antibody, gameObject), false);
                }
            }

            detectedSoundManager.CreateAudioSrc();
            detectedSoundManager.PlaySound();
        }
    }

    public override void StopAttack(){
        isAlerted = false;
    }
}
