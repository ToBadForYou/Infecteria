using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Organ
{
    public float nextScout = 5;
    public int scoutProductionTime = 5;
    public int maxReportAntibodies = 6;

    public UnitProducer alertedProducer;

    new void Start(){
        base.Start();
        alertedProducer.AddProduction(UnitType.ANTIBODY, new UnitProductionData(20, 50, 20));
    }

    new void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            base.Update();
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
            DeployAntibodies(alertPosition);
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
