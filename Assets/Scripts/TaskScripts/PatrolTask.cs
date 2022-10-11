using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTask : Task
{ 
    Vector2 start;
    Vector2 end;
    Vector2 goal;

    public PatrolTask(Unit owner, Vector2 targetStart, Vector2 targetEnd) : base(owner, null) {
        taskType = TaskType.PATROL;
        goal = targetStart;
        start = targetStart;
        end = targetEnd;
    }

    public override void Update(){
        if (!unit.IsMoving()){
            unit.MoveToPosition(goal);
        }

        if(unit.AtPosition(goal)){
            if(goal == start){
                goal = end;
            }
            else{
                goal = start;
            }
        }
    }
}
