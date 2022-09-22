using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : InfectUnit
{
    public Factory parent;

    public bool followPlayer;
    private GameObject player;

    public Cell targetCell;
    public Vector2 startPosition;

    void Start() {
        startPosition = transform.position;
        SetUnitStats(10, 10, 1, 1, 1.0f, 0.9f, true);
        player = GameObject.Find("Player");
    }

    public override void OnReachedDestination(GameObject target){

    }

    public override void InfectTarget(Infectable target){
        target.Infect(infectionAmount);
        parent.RemoveMicrobacteria(this);
        Destroy(gameObject);
    }

    public override void OnDeath() {
        parent.RemoveMicrobacteria(this);
    }
}
