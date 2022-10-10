using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointNode : Unit
{
    Checkpoint checkpoint;
    UnitSpawner unitSpawner;
    int currentAntibodies = 5;
    public int maxStoredAntibodies = 5;
    int antibodyProductionTime = 15;
    float nextAntibody = 0;

    new void Start(){
        base.Start();
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        SetUnitStats(50, 50, 0, 1, 1.0f, 0.2f, false);
    }

    new void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            nextAntibody -= Time.deltaTime;
            if(currentAntibodies < maxStoredAntibodies && nextAntibody < 0){
                nextAntibody = antibodyProductionTime;
                currentAntibodies += 1;
            }
        }
    }

    public void SetCheckpoint(Checkpoint parent){
        checkpoint = parent;
    }

    public override void OnDeath(){
        checkpoint.RemoveNode(gameObject);
        checkpoint.TakeDamage();
    }

    void TriggerAntibodies(Vector2 alertPosition){
        if(currentAntibodies > 0){
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
            currentAntibodies = 0;
            UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
            newSquad.MoveTo(alertPosition, false);
            //foreach (Unit antibody in antibodies){
             //   antibody.GiveTask(new ReturnTask(antibody, gameObject));
           // } 
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        Unit colUnit = col.gameObject.GetComponent<Unit>();
        if(colUnit != null && IsHostile(colUnit)){
            TriggerAntibodies(col.gameObject.transform.position);
        }
    }    
}
