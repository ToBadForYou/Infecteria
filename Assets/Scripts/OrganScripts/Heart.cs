using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Organ
{
    public float nextScout = 5;
    public int scoutProductionTime = 5;
    public int maxReportAntibodies = 6;
    public int minimumSpawn = 5;

    public UnitProducer alertedProducer;

    new void Start(){
        base.Start();
        alertedProducer.AddProduction(UnitType.ANTIBODY, new UnitProductionData(20, 50, 20));
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            nextScout -= Time.deltaTime;
            if (nextScout < 0){
                nextScout = scoutProductionTime;
                Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                newScout.SetTarget(gm.cells[Random.Range(0, gm.cells.Count)]);
            }
        }
    }

    public void OnReport(Vector2 alertPosition){
        if(Vector2.Distance(transform.position, alertPosition) < 14){
            int currentAntibodies = unitProducer.GetAvailableAmount(UnitType.ANTIBODY);
            if(currentAntibodies > minimumSpawn){
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
        }
        else {
            int randomAmount = Random.Range(2, maxReportAntibodies);
            int currentAntibodies = alertedProducer.GetAvailableAmount(UnitType.ANTIBODY);
            if(randomAmount <= currentAntibodies){
                currentAntibodies = alertedProducer.WithdrawAmount(UnitType.ANTIBODY, randomAmount);
                List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
                Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
                UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
                newSquad.AddUnit(newScout);
                unitSquads.Add(newSquad);
                AI behaviour = new AlertedPatrol(newSquad, alertPosition, newScout);
                newSquad.AIBehaviour = behaviour;
            }
        }
    } 
}
