using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : Unit
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

    void Update()
    {
        base.Update();
        if(followPlayer && !targetCell) {
            unitMovement.FollowTarget(player);
        }
        else if(isSelected) {
            if(targetCell && !unitMovement.moving) {
                unitMovement.FollowTarget(targetCell.gameObject);
            }
        }
    }

    public override void OnReachedDestination(GameObject target){
        if(followPlayer && !targetCell) {

        }
        else if(target != null && target == targetCell.gameObject){
            targetCell.Infect(0.2f);
            parent.RemoveMicrobacteria(this);
            Destroy(gameObject);
        }
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
