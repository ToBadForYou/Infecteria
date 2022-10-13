using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectUnit : Unit
{
    public float infectionAmount;

    public virtual void InfectTarget(Infectable target){

    }

    public virtual bool CanInfect(Infectable target){
        return false;
    }
}
