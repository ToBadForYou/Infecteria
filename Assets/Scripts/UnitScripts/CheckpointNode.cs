using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointNode : Unit
{
    Checkpoint checkpoint;
    UnitSpawner unitSpawner;
    public UnitProducer unitProducer;

    new void Start(){
        base.Start();
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        SetUnitStats(50, 50, 0, 1, 1.0f, 0.2f, false);
        unitProducer.AddProduction(UnitType.ANTIBODY, new UnitProductionData(5, 5, 20));
    }

    public void SetCheckpoint(Checkpoint parent){
        checkpoint = parent;
    }

    public override void OnDeath(){
        checkpoint.RemoveNode(gameObject);
        checkpoint.TakeDamage();
    }

    void TriggerAntibodies(Vector2 alertPosition){
        int currentAntibodies = unitProducer.WithdrawAll(UnitType.ANTIBODY);
        if(currentAntibodies > 0){
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
            UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
            newSquad.MoveTo(transform.position, false);
            foreach (Unit antibody in antibodies){
                antibody.producer = unitProducer;
                antibody.GiveTask(new ReturnTask(antibody, gameObject), false);
            } 
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        Unit colUnit = col.gameObject.GetComponent<Unit>();
        if(colUnit != null && IsHostile(colUnit)){
            TriggerAntibodies(col.gameObject.transform.position);
        }
    }    
}
