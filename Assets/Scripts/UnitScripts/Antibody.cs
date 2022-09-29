using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : InfectUnit
{
    new void Start() {
        base.Start();
        SetUnitStats(10, 10, 1, 1, 1.0f, 1.0f, true);
    }

    public override void InfectTarget(Infectable target){
        target.Infect(infectionAmount);
        Destroy(gameObject);
    }

    public override void OnReachedDestination(GameObject target){
        
    }
}
