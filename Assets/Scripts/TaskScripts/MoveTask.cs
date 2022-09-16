using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTask : Task
{ 
    new Vector2 target;

    public MoveTask(Unit owner, Vector2 targetVector) : base(owner) {
        target = targetVector;
    }

    public override void Update(){
        if (!unit.IsMoving()){
            unit.MoveToPosition(target);
        }
        if(unit.AtPosition(target)){
            Debug.Log("Finished!");
            FinishTask();
        }
    }
}
