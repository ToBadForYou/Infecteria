using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : InfectUnit
{
    public Factory parent;

    public bool followPlayer;
    private GameObject player;

    public bool isSelected;
    public Cell targetCell;
    public Vector2 startPosition;

    void Start() {
        player = GameObject.Find("Player");
    }

    public override void OnReachedDestination(GameObject target){

    }

    public override void InfectTarget(Infectable target){
        target.Infect(infectionAmount);
        parent.RemoveMicrobacteria(this);
        Destroy(gameObject);
    }

    public void ToggleSelection() {
        isSelected = !isSelected;
        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(!child.activeSelf);
    }

    public override void OnDeath() {
        parent.RemoveMicrobacteria(this);
    }
}
