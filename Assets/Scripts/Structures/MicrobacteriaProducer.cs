using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrobacteriaProducer : Structure
{
    public Transform barPivot;
    public UnitProducer unitProducer;
    UnitSpawner unitSpawner;
    GameManager gm;
    
    void Start(){
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        unitProducer.AddProduction(UnitType.MICROBACTERIA, new UnitProductionData(0, 1, 15));
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            barPivot.localScale = new Vector2(1.0f - unitProducer.GetProductionProgress(UnitType.MICROBACTERIA), 1.0f);
            int withdrawAmount = unitProducer.GetAvailableAmount(UnitType.MICROBACTERIA);
            if(withdrawAmount > 0){
                if(builtBy.mouseObject != null){
                    if(!builtBy.mouseObject.activeSelf)
                        builtBy.MakeObjActive(builtBy.mouseObject);
                }
                withdrawAmount = unitProducer.WithdrawAmount(UnitType.MICROBACTERIA, 1);
                gm.producedMicrobacterias++;
                MicroBacteria newBacteria = unitSpawner.SpawnMicrobacteria(transform.position, unitProducer);                
                builtBy.AddMicrobacteria(newBacteria);
                if(builtBy.infectTarget != null){
                    newBacteria.GiveTask(new InfectTask(newBacteria, builtBy.infectTarget.gameObject), false);
                }
                else {
                    newBacteria.GiveTask(new MoveTask(newBacteria, new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f))), false);
                }
            }
        }
    }

    public int GetBacteriaAmount(){
        return unitProducer.GetSpawnedAmount(UnitType.MICROBACTERIA);
    }

    public int GetMaximumAmount(){
        return unitProducer.GetMaximumAmount(UnitType.MICROBACTERIA);
    }   
}
