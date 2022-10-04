using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectTask : Task
{
    new InfectUnit unit;

    public InfectTask(InfectUnit owner, GameObject targetObject) : base((Unit)owner, targetObject) {
        taskType = TaskType.INFECT;
        unit = owner;
        target = targetObject;
    }    

    public override void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if (!unit.IsMoving())
                unit.FollowTarget(target);
            
            if(unit.AtPosition(target.transform.position)) {
                Infectable infectTarget = target.GetComponent<Infectable>();
                if(infectTarget != null)
                    unit.InfectTarget(infectTarget);
                FinishTask();
            }
        }
    } 
}
