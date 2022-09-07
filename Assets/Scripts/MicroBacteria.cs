using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroBacteria : MonoBehaviour
{
    public float speed = 5.0f;
    public bool isSelected;
    public Cell targetCell;
    public Vector2 startPosition;

    void Start() {
        //Temporary setting selected to true and choosing a random target cell
        //isSelected = true;
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        targetCell = cells[Random.Range(0,cells.Length)].GetComponent<Cell>();
    }

    void Update()
    {
        if(isSelected) {
            if(targetCell) {
                float step = speed * Time.deltaTime;
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
        else {
            if(Vector2.Distance(transform.position, startPosition) >= 0.01f) {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, startPosition, step);
            }
        }
    }

    public void ToggleSelection() {
        isSelected = !isSelected;
        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(!child.activeSelf);
    }

    void OnMouseDown() {
        ToggleSelection();
    }
}
