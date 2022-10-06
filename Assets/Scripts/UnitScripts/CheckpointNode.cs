using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointNode : Unit
{
    new void Start(){
        base.Start();
        SetUnitStats(50, 50, 0, 1, 1.0f, 0.2f, false);
    }
}
