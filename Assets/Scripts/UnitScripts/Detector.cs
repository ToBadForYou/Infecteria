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
    public int maxAntibodies = 3;
    UnitSpawner unitSpawner;
    
    void Start()
    {
        reportTo = GameObject.Find("Heart");
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
    }

    public override void Attack(){
        if (!isAlerted){
            isAlerted = true;
            Scout newScout = unitSpawner.SpawnScout(transform.position, reportTo);
            newScout.SetAlerted(gameObject);
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, Random.Range(1, maxAntibodies));
            unitSquad.AddUnits(antibodies);
        }
    }

    public override void StopAttack(){
        
    }
}
