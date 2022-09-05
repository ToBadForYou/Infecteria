using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2 originPos;
    private Vector2 target;

    public Transform infectionBarPivot;

    public float offset;
    public float speed;

    public float maxInfectionAmount;
    public float infectionAmount;
    public bool isInfected;

    void Start()
    {
        originPos = transform.position;
        target = GetNewTarget();
    }

    public void GetAbsorbed() {
        
    }

    public void TurnIntoFactory() {

    }

    public void Infect(float amount) {
        if(!isInfected){
            infectionAmount += amount;
            if(infectionAmount >= maxInfectionAmount) {
                infectionAmount = maxInfectionAmount;
                isInfected = true;
            }
            infectionBarPivot.localScale = new Vector2(infectionAmount/10.0f, 1.0f);
        }
    }

    Vector2 GetNewTarget() {
        return new Vector2(Random.Range(originPos.x-offset, originPos.x+offset), Random.Range(originPos.y-offset, originPos.y+offset));
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        
        float diff = Vector2.Distance(transform.position, target);
        if(diff <= 0.01f) {
            target = GetNewTarget();
        }
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(2.5f);
            col.gameObject.GetComponent<Player>().currentCell = this;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(5.0f);
            col.gameObject.GetComponent<Player>().currentCell = null;
        }
    }
}
