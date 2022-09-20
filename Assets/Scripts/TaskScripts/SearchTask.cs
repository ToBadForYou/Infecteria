using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTask : Task
{
    new InfectUnit unit;
    List<Infectable> searchList;

    public SearchTask(InfectUnit owner, List<Infectable> potentialInfections) : base(owner, null) {
        unit = owner;
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
                    unit.InfectTarget(infectable);
                }
                target = null;
            }
        }
    }
}
