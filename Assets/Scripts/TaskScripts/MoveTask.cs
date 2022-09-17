using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTask : Task
{ 
    Vector2 targetPosition;

    public MoveTask(Unit owner, Vector2 targetVector) : base(owner) {
        targetPosition = targetVector;
    }

    public MoveTask(Unit owner, GameObject targetObject) : base(owner) {
        target = targetObject;
    }    

    public override void Update(){
        bool objectTarget = target != null;
        if (!unit.IsMoving()){
            if(objectTarget){
                unit.FollowTarget(target);
            }
            else {
                unit.MoveToPosition(targetPosition);
            }
        }
        
        Vector2 targetPos;
        if(objectTarget){
            targetPos = target.transform.position;
        }
        else {
            targetPos = targetPosition;
        }
        if(unit.AtPosition(targetPos)){
            FinishTask();
        }
    }
}
