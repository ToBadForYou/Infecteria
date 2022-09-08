using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : RangedUnit
{
    public GameObject parent;
    public GameObject exclamationMark;
    public GameObject targetCell;
    public GameObject alertObject;
    public bool finished;
    public bool isAlerted;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Attack(){
        if (!isAlerted && target.tag == "Player"){
            SetAlerted(target);
        }
    }

    public void SetAlerted(GameObject triggerObject){
        isAlerted = true;
        exclamationMark.SetActive(true);
        unitMovement.MoveToTarget(parent);
        finished = true; 
        alertObject = triggerObject;  
    }

    public void SetTarget(GameObject target){
        unitMovement.MoveToTarget(target);
        targetCell = target;
    }

    public override void OnReachedDestination(GameObject target){
        if(target == targetCell){
            unitMovement.MoveToTarget(parent);
            finished = true;
        }

        if(finished && target == parent) {
            if(isAlerted){
                parent.GetComponent<Heart>().OnReport(alertObject);
            }
            Destroy(transform.parent.gameObject); 
        }      
    }
}
