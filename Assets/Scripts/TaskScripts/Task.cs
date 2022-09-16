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
        Debug.Log("Calling here?");
    }

    public void FinishTask(){
        Debug.Log("I'm done");
        finished = true;
    }
}
