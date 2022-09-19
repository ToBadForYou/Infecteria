using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject followTarget;
    public Vector2 positionTarget;
    public Vector2 offset;
    public Unit ownerUnit;

    public float speed = 2.0f; // TODO Read speed value from owner object
    public bool moving;

    void Start()
    {
        offset = Vector2.zero;
    }

    void Update()
    {
        if(moving) {
            Vector2 targetPosition = positionTarget;
            if(followTarget != null){
                targetPosition = followTarget.transform.position;
            }
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition + offset, step);
            if(Vector2.Distance(transform.position, targetPosition + offset) <= 0.2f) {
                moving = false;
                ownerUnit.OnReachedDestination(followTarget);
            }
        }       
    }

    public void MoveToPosition(Vector2 pos){
        positionTarget = pos;
        followTarget = null;
        offset = Vector2.zero;
        moving = true;
    }

    public void FollowTarget(GameObject newTarget){
        followTarget = newTarget;
        offset = Vector2.zero;
        moving = true;
    }

    public void FollowTarget(GameObject newTarget, Vector2 targetOffset){
        followTarget = newTarget;
        offset = targetOffset;
        moving = true;
    }

    public void StopMoving(){
        moving = false;
    }
}
