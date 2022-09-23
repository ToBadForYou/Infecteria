using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    private bool isDown;
    private bool isTargeting;

    public GameObject fakeCellPrefab;
    public GameObject windowPrefab;
    public GameObject controlPanel;

    public Transform selectionTransform;
    public float targetCellRadius;

    private Vector2 topLeft;
    private Vector2 bottomRight;
    public UnitSquad selectedUnits;

    void SelectUnits() {
        List<MicroBacteria> bacterias = new List<MicroBacteria>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Bacteria");

        // Handle selection directions
        if(topLeft.x > bottomRight.x && topLeft.y < bottomRight.y) {
            //topLeft is infact bottomRight, so flip
            Vector2 oldTopLeft = topLeft;
            topLeft = bottomRight;
            bottomRight = oldTopLeft;
        }
        else if(topLeft.x > bottomRight.x && topLeft.y > bottomRight.y) {
            //topLeft is infact topRight
            float leftX = bottomRight.x;
            float rightX = topLeft.x;
            topLeft.x = leftX;
            bottomRight.x = rightX;
        }
        else if(topLeft.x < bottomRight.x && topLeft.y < bottomRight.y) {
            //topLeft is infact bottomLeft
            float leftY = bottomRight.y;
            float rightY = topLeft.y;
            topLeft.y = leftY;
            bottomRight.y = rightY;
        }

        for(int i = 0; i < objs.Length; i++) {
            if(objs[i].transform.position.x >= topLeft.x && objs[i].transform.position.x <= bottomRight.x) {
                if(objs[i].transform.position.y <= topLeft.y && objs[i].transform.position.y >= bottomRight.y) {
                    Unit bacteria = objs[i].GetComponent<Unit>();
                    bacteria.ToggleSelection(true);
                    selectedUnits.AddUnit(bacteria);
                }
            }
        }
    }

    void DeselectAllBacterias() {
        selectedUnits.ToggleSelection(false);
        selectedUnits.Clear();
    }

    public void AssignFollowTask() {
        //TODO: Add to player units squad
        selectedUnits.Follow(GameObject.Find("Player"));
        DeselectAllBacterias();
    }

    public void AssignReturnTask() {
        //TODO: Use ReturnTask (currently only supports antibodies)
        //Temp use first index values start position
        selectedUnits.MoveTo(((MicroBacteria)selectedUnits.units[0]).startPosition);
        DeselectAllBacterias();
    }

    public void AssignMoveTask() {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        selectedUnits.MoveTo(mousePosition);
        DeselectAllBacterias();
    }

    public void AssignInfectTask() {
        //TODO: Make selected microbacterias infect chosen cell
        DeselectAllBacterias();
    }

    void Update(){
        if(selectedUnits.units.Count > 0){

            if(!controlPanel.activeSelf) {
                controlPanel.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.I)) { // Infect cell
                AssignInfectTask();
            }
            else if(Input.GetKeyDown(KeyCode.F)) { // Follow player
                AssignFollowTask();
            }
            else if(Input.GetKeyDown(KeyCode.M)) { // Move to mouse position
                AssignMoveTask();
            }
            else if(Input.GetKeyDown(KeyCode.R)) { // Return to factory
                AssignReturnTask();
            }
        }
        else {
            if(controlPanel.activeSelf) {
                controlPanel.SetActive(false);
            }
        }

        if(Input.GetMouseButtonDown(1)) {
            selectionTransform.gameObject.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);
            topLeft = mousePosition;
            
            isDown = true;
        }
        else if(Input.GetMouseButtonUp(1)) {
            selectionTransform.gameObject.SetActive(false);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);
            bottomRight = mousePosition;

            DeselectAllBacterias();
            SelectUnits();

            isDown = false;
        }
        if(isDown) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float xScale = Mathf.Abs(topLeft.x - worldPosition.x);
            float yScale = Mathf.Abs(topLeft.y - worldPosition.y);

            selectionTransform.localScale = new Vector2(xScale, yScale);
            
            //Draw selection box
            if(worldPosition.x > topLeft.x && worldPosition.y < topLeft.y) {
                selectionTransform.position = new Vector2(worldPosition.x - xScale/2.0f, worldPosition.y + yScale/2.0f);
            }
            else if(worldPosition.x < topLeft.x && worldPosition.y > topLeft.y) {
                selectionTransform.position = new Vector2(worldPosition.x + xScale/2.0f, worldPosition.y - yScale/2.0f);
            }
            else if(worldPosition.x > topLeft.x && worldPosition.y > topLeft.y) {
                selectionTransform.position = new Vector2(worldPosition.x - xScale/2.0f, worldPosition.y - yScale/2.0f);
            }
            else {
                selectionTransform.position = new Vector2(worldPosition.x + xScale/2.0f, worldPosition.y + yScale/2.0f);
            }
        }
    }
}
