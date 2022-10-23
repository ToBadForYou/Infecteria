using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : InfectUnit
{
    public Vector2 startPosition;

    new void Start(){
        base.Start();
        startPosition = transform.position;
        SetUnitStats(10, 10, 1, 1, 1.0f, 0.9f, true);
    }

    public override void OnReachedDestination(GameObject target){

    }

    public override void InfectTarget(Infectable target){
        target.Infect(infectionAmount);
        OnDeath();
        Destroy(gameObject);
    }

    public override bool CanInfect(Infectable target){
        return !target.isInfected;
    }    
}
