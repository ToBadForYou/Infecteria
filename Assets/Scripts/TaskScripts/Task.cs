using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public Unit unit;
    public bool finished;
    public GameObject target;
    public TaskType taskType = TaskType.IDLE;

    protected Task(Unit owner, GameObject targetObject){
        if(owner.CanMove()){
            owner.StopMoving();
        }
        unit = owner;
        target = targetObject;
    }

    public GameObject GetTarget(){
        return target;
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
