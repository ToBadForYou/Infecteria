using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : Unit
{
    public bool isSelected;
    public Cell targetCell;
    public Vector2 startPosition;

    void Update()
    {
        base.Update();
        if(isSelected) {
            if(targetCell && !unitMovement.moving) {
                unitMovement.FollowTarget(targetCell.gameObject);
            }
        }
    }

    public override void OnReachedDestination(GameObject target){
        if(target != null && target == targetCell.gameObject){
            targetCell.Infect(0.2f);
            Destroy(gameObject);
        }
    }

    public void ToggleSelection() {
        isSelected = !isSelected;
        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(!child.activeSelf);
    }
}
