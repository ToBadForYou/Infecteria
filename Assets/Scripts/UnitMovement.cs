using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject target;
    public Unit owner;
    public float speed = 2.0f; // TODO Read speed value from owner object
    public bool moving;

    void Start()
    {
    }

    void Update()
    {
        if(target != null && moving) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if(Vector2.Distance(transform.position, target.transform.position) <= 0.2f) {
                moving = false;
                owner.OnReachedDestination(target);
            }
        }       
    }

    public void MoveToTarget(GameObject newTarget){
        target = newTarget;
        moving = true;
    }

    public void StopMoving(){
        moving = false;
    }
}
