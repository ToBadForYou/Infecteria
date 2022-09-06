using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : MonoBehaviour
{
    public bool isSelected;
    public Cell targetCell;

    void Update()
    {
        if(isSelected) {
            if(targetCell) {
                float step = 5.0f * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetCell.transform.position, step);
                if(Vector2.Distance(transform.position, targetCell.transform.position) <= 0.01f) { 
                    targetCell.Infect(0.2f);
                    Destroy(gameObject); 
                }
            }
            else {
                //Listen for mouse click for targetCell (maybe open some sort of mimimap with nearby cells)
                //Set target cell to clicked cell
            }
        }
    }

    void OnMouseDown() {
        isSelected = !isSelected;
    }
}
