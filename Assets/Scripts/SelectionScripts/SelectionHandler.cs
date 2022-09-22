using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    private bool isDown;
    private bool isTargeting;

    public GameObject fakeCellPrefab;
    public GameObject windowPrefab;

    public Transform selectionTransform;
    public float targetCellRadius;

    private Vector2 topLeft;
    private Vector2 bottomRight;

    private List<MicroBacteria> selectedMicroBacterias = new List<MicroBacteria>();

    List<MicroBacteria> GetSelectedBacterias() {
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
                    bacterias.Add(objs[i].GetComponent<MicroBacteria>());
                }
            }
        }

        return bacterias;
    }

    void DeselectAllBacterias() {
        foreach(MicroBacteria bacteria in selectedMicroBacterias){ 
            bacteria.ToggleSelection(false);
        }
        selectedMicroBacterias = new List<MicroBacteria>();
    }

    void AssignFollowTask() {
        GameObject playerUnit = GameObject.Find("Player");
        foreach(MicroBacteria bacteria in selectedMicroBacterias){
            bacteria.CancelTasks();
            Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            bacteria.GiveTask(new FollowTask(bacteria, playerUnit, offset));
        }
        DeselectAllBacterias();
    }

    void AssignReturnTask() {
        foreach(MicroBacteria bacteria in selectedMicroBacterias){
            bacteria.CancelTasks();
            bacteria.GiveTask(new MoveTask(bacteria, bacteria.startPosition));
        }
        DeselectAllBacterias();
    }

    void AssignMoveTask() {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        foreach(MicroBacteria bacteria in selectedMicroBacterias){
            bacteria.CancelTasks();
            Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            bacteria.GiveTask(new MoveTask(bacteria, mousePosition + offset));
        }
        DeselectAllBacterias();
    }

    void AssignInfectTask() {
        //TODO: Make selected microbacterias infect chosen cell
        DeselectAllBacterias();
    }

    void Update()
    {
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
            selectedMicroBacterias = GetSelectedBacterias();
            foreach (MicroBacteria bacteria in selectedMicroBacterias) {
                bacteria.ToggleSelection(true);
            }

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
