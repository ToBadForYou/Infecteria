using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : RangedUnit
{
    public GameObject parent;
    public GameObject exclamationMark;
    public GameObject targetCell;
    public bool finished;
    public bool isAlerted;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Attack(){
        if (!isAlerted && target.tag == "Player"){
            SetAlerted();
        }
    }

    public void SetAlerted(){
        isAlerted = true;
        exclamationMark.SetActive(true);
        unitMovement.MoveToTarget(parent);
        finished = true;        
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
            Destroy(transform.parent.gameObject); 
        }      
    }
}
