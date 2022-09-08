using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public GameObject target;
    public Unit owner;
    public float speed = 2.0f; // TODO Read speed value from owner object
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && moving) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if(Vector2.Distance(transform.position, target.transform.position) <= 0.01f) {
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
