using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Organ
{
    public float nextScout = 5;
    public int scoutProductionTime = 5;
    public int maxReportAntibodies = 6;

    new void Update()
    {
        base.Update();
        nextScout -= Time.deltaTime;
        if (nextScout < 0){
            nextScout = scoutProductionTime;
            Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            newScout.SetTarget(gm.cells[Random.Range(0, gm.cells.Count)]);
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
        if(randomAmount <= currentAntibodies){
            currentAntibodies -= randomAmount;
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, randomAmount);
            Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
            UnitSquad newSquad = CreateSquad(antibodies);
            newSquad.AddUnit(newScout);
            newSquad.MoveTo(alertPosition);
            newSquad.Search(alertPosition, newScout);
        }
    } 
}
