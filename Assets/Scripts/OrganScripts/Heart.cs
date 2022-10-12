using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Organ
{
    public float nextScout = 5;
    public int scoutProductionTime = 5;
    public int maxReportAntibodies = 6;
    public int minimumSpawn = 5;

    public int scoutAntibodies = 20;
    public int maxScoutAntibodies = 50;
    public float nextScoutAntibody = 5;
    public int scoutAntibodyProductionTime = 30;

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            nextScout -= Time.deltaTime;
            nextScoutAntibody -= Time.deltaTime;
            if (nextScout < 0){
                nextScout = scoutProductionTime;
                Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                newScout.SetTarget(gm.cells[Random.Range(0, gm.cells.Count)]);
            }
            if (scoutAntibodies < maxScoutAntibodies && nextScoutAntibody < 0){
                nextScoutAntibody = scoutAntibodyProductionTime;
                scoutAntibodies += 1;
            }
        }
    }

    public void OnReport(Vector2 alertPosition){
        int randomAmount = Random.Range(2, maxReportAntibodies);
        int currentAntibodies = unitProducer.GetAmount(UnitType.ANTIBODY);
        if(currentAntibodies > minimumSpawn && Vector2.Distance(transform.position, alertPosition) < 14){
            currentAntibodies = unitProducer.WithdrawAll(UnitType.ANTIBODY);
            // TODO Recall all antibodies
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
            UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
            unitSquads.Add(newSquad);
            newSquad.MoveTo(alertPosition, false);
            foreach (Unit antibody in antibodies){
                antibody.producer = unitProducer;
                antibody.GiveTask(new ReturnTask(antibody, gameObject), false);
            } 
        }
        else if(randomAmount <= scoutAntibodies){
            scoutAntibodies -= randomAmount;
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, randomAmount);
            Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
            UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
            newSquad.AddUnit(newScout);
            unitSquads.Add(newSquad);
            AI behaviour = new AlertedPatrol(newSquad, alertPosition, newScout);
            newSquad.AIBehaviour = behaviour;
        }
    } 
}
