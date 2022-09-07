using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Tower
{
    public MoveToTarget moveToTarget;
    public GameObject parent;
    public GameObject exclamationMark;
    public GameObject targetCell;
    public bool finished;
    public bool isAlerted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        base.Update();
        if(moveToTarget && moveToTarget.target == null){
            moveToTarget.target = parent.transform;
            finished = true;
        }
        if(finished && Vector2.Distance(transform.position, parent.transform.position) <= 0.01f) { 
            Destroy(gameObject); 
        }
    }

    override public void Attack(){
        if (!isAlerted && target.tag == "Player"){
            isAlerted = true;
            moveToTarget.target = parent.transform;
            exclamationMark.SetActive(true);
            finished = true;
        }
    }

    public void SetTarget(GameObject target){
        moveToTarget.target = target.transform;
        targetCell = target;
    }
}
