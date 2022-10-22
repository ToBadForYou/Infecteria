using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTask : Task
{
    new Scout unit;
    List<Infectable> searchList;
    UnitSquad ownerSquad;

    public SearchTask(Scout owner, List<Infectable> potentialInfections, UnitSquad belongTo) : base(owner, null) {
        taskType = TaskType.SEARCH;
        unit = owner;
        ownerSquad = belongTo;
        searchList = potentialInfections;
    }

    public override void Update(){
        if(target == null){
            if(searchList.Count > 1){
                target = searchList[0].gameObject;
                searchList.RemoveAt(0);
            }
            else {
                FinishTask();
            }
        }
        else {
            if(!unit.IsMoving()){
                unit.FollowTarget(target);
            }
            if(unit.AtPosition(target.transform.position)){
                Infectable infectable = target.GetComponent<Infectable>();
                if(infectable.isInfected || infectable.infectionAmount/infectable.maxInfectionAmount > 0.5){
                    ownerSquad.Infect(infectable, true);
                    unit.GiveTask(new ReturnTask(unit, unit.parent), false);
                    FinishTask();
                }
                target = null;
            }
        }
    }
}
