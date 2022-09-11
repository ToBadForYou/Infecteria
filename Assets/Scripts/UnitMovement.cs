using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject followTarget;
    public Vector2 positionTarget;
    public Unit ownerUnit;
    public float speed = 2.0f; // TODO Read speed value from owner object
    public bool moving;

    void Start()
    {
    }

    void Update()
    {
        if(moving) {
            Vector2 targetPosition = positionTarget;
            if(followTarget != null){
                targetPosition = followTarget.transform.position;
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
            if(Vector2.Distance(transform.position, targetPosition) <= 0.2f) {
                moving = false;
                ownerUnit.OnReachedDestination(followTarget);
            }
        }       
    }

    public void MoveToPosition(Vector2 pos){
        positionTarget = pos;
        followTarget = null;
        moving = true;
    }

    public void FollowTarget(GameObject newTarget){
        followTarget = newTarget;
        moving = true;
    }

    public void StopMoving(){
        moving = false;
    }
}
