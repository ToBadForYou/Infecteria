using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public Unit unit;
    public bool finished;
    public GameObject target;

    protected Task(Unit owner){
        owner.StopMoving();
        unit = owner;
    }

    public void SetTarget(GameObject gameObject){
        target = gameObject;
    }

    public virtual void Update(){

    }

    public void FinishTask(){
        finished = true;
    }
}
