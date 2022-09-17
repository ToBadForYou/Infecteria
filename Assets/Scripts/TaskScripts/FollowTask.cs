using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTask : Task
{
    public FollowTask(Unit owner, GameObject targetObject) : base(owner, targetObject) {}

    public override void Update(){
        unit.FollowTarget(target);
    }    
}
