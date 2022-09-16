using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public Unit unit;
    public bool finished;
    public GameObject target;

    public void SetTarget(GameObject gameObject){
        target = gameObject;
    }

    public void FinishTask(){
        Destroy(this);
    }
}
