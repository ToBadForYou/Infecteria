using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTask : Task
{

    void Update(){
        if (target != null){
            bool targetInRange = unit.InRange(target.transform.position);
            if (!unit.IsMoving() && !targetInRange){
                unit.FollowTarget(target);
            } else if (targetInRange) {
                if(unit.IsMoving()){
                    unit.StopMoving();
                }
                if (unit.AttackTarget(target)){
                    FinishTask();
                }
            }
        }
    }

    public GameObject GetTarget(){
        return target;
    }
}
