using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTask : Task
{
    public AttackTask(Unit owner, GameObject targetObject) : base(owner, targetObject) {
        taskType = TaskType.ATTACK;
    }

    public override void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if (target != null){
                bool targetInRange = unit.InRange(target.transform.position);
                if (unit.CanMove() && !unit.IsMoving() && !targetInRange){
                    unit.FollowTarget(target);
                } else if (targetInRange) {
                    if(unit.CanMove() && unit.IsMoving())
                        unit.StopMoving();
                    if (unit.AttackTarget(target))
                        FinishTask();
                }
            }
            else{
                FinishTask();
            }
        }
    }
}
