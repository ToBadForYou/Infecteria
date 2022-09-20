using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Organ
{
    public float scoutSpawnTimer = 5;
    public int baseScoutSpawnTime = 5;
    public int maxAntibodies = 6;

    void Update()
    {
        scoutSpawnTimer -= Time.deltaTime;
        if (scoutSpawnTimer < 0){
            scoutSpawnTimer = baseScoutSpawnTime;
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
        List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, Random.Range(2, maxAntibodies));
        UnitSquad newSquad = CreateSquad(antibodies);
        newSquad.MoveTo(alertPosition);
        newSquad.Search(alertPosition);
    }
}
