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

    new void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            base.Update();
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

    UnitSquad CreateSquad(List<Unit> units){
        GameObject tempObj = new GameObject("UnitSquad");
        UnitSquad newUnitSquad = tempObj.AddComponent<UnitSquad>();
        newUnitSquad.AddUnits(units);
        unitSquads.Add(newUnitSquad);
        return newUnitSquad;
    }

    public void OnReport(Vector2 alertPosition){
        int randomAmount = Random.Range(2, maxReportAntibodies);
        // TODO Add a limit or different source of scouting antibodies
        // to ensure not being able to exhaust organs by being detected far away
        if(currentAntibodies > minimumSpawn && Vector2.Distance(transform.position, alertPosition) < 14){
            // TODO Recall all antibodies
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
            currentAntibodies = 0;
            UnitSquad newSquad = CreateSquad(antibodies);
            newSquad.MoveTo(alertPosition, false);
            foreach (Unit antibody in antibodies){
                antibody.GiveTask(new ReturnTask(antibody, gameObject));
            } 
        }
        else if(randomAmount <= scoutAntibodies){
            scoutAntibodies -= randomAmount;
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, randomAmount);
            Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
            UnitSquad newSquad = CreateSquad(antibodies);
            newSquad.AddUnit(newScout);
            AI behaviour = new AlertedPatrol(newSquad, alertPosition, newScout);
            newSquad.AIBehaviour = behaviour;
        }
    } 
}
