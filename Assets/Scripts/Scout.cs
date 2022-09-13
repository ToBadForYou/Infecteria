using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : DetectorUnit
{
    public GameObject parent;
    public GameObject exclamationMark;
    public GameObject targetCell;
    public Vector2 alertPosition;
    public bool finished;
    public bool isAlerted;

    public override void Attack(){
        if (!isAlerted){
            SetAlerted(target);
        }
    }

    public void SetAlerted(GameObject triggerObject){
        isAlerted = true;
        exclamationMark.SetActive(true);
        unitMovement.FollowTarget(parent);
        finished = true; 
        alertPosition = triggerObject.transform.position;  
    }

    public void SetTarget(GameObject target){
        unitMovement.FollowTarget(target);
        targetCell = target;
    }

    public override void OnReachedDestination(GameObject target){
        if (target == null){
            unitMovement.FollowTarget(parent);
            finished = true;   
        }
        if(target == targetCell){
            Infectable cell = targetCell.GetComponent<Infectable>();
            if (cell != null && (cell.isInfected || cell.infectionAmount/cell.maxInfectionAmount > 0.5)){
                // TODO heal the cell
                SetAlerted(targetCell);
            }
            else {
                unitMovement.FollowTarget(parent);
                finished = true;
            }
        }

        if(finished && target == parent) {
            if(isAlerted){
                parent.GetComponent<Heart>().OnReport(alertPosition);
            }
            Destroy(transform.parent.gameObject); 
        }      
    }
}
