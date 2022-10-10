using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTask : Task
{
    public ReturnTask(Unit owner, GameObject target) : base(owner, target) {
        taskType = TaskType.RETURN;
    }

    public override void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(!unit.IsMoving())
                unit.FollowTarget(target);

            if(unit.AtPosition(target.transform.position)){
                UnitProducer producer = target.GetComponent<UnitProducer>();
                if(producer != null)
                    producer.OnUnitReturn(unit);
                FinishTask();
            }
        }
    }    
}
