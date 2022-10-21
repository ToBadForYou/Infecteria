using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : InfectUnit
{
    new void Start() {
        base.Start();
        SetUnitStats(25, 25, 2, 1.5f, 1.0f, 1.0f, true);
    }

    public override void InfectTarget(Infectable target){
        target.Infect(infectionAmount);
        Destroy(gameObject);
    }

    public override bool CanInfect(Infectable target){
        return target.isInfected || target.infectionAmount > 0;
    }

    public override void OnReachedDestination(GameObject target){
        
    }
}
