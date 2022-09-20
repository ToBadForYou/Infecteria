using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : InfectUnit
{
    void Start() {
        SetUnitStats(10, 10, 1, 1, 1.0f, 1.0f, true);
    }

    public override void OnReachedDestination(GameObject target){
        
    }
}
