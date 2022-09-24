using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTask : Task
{
    public ReturnTask(Unit owner, GameObject target) : base(owner, target) {
        taskType = TaskType.RETURN;
    }

    public override void Update(){
        if(!unit.IsMoving()){
            unit.FollowTarget(target);
        }
        if(unit.AtPosition(target.transform.position)){
            Organ organ = target.GetComponent<Organ>();
            if(organ != null){
                organ.OnUnitReturn(unit);
            }
            FinishTask();
        }
    }    
}
