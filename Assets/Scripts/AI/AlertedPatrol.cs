using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertedPatrol: AI
{
    UnitSquad units;
    Scout scout;

    public AlertedPatrol(UnitSquad alertedUnits, Vector2 alertPosition, Scout searchScout){
        units = alertedUnits;
        scout = searchScout;
        units.MoveTo(alertPosition, false);
        units.Search(alertPosition, searchScout);       
    }

    public override void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            if(scout.GetTaskType() == TaskType.SEARCH){
                
            }
        }
    }
}