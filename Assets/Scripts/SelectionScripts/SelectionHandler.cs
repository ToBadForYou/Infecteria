using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    void DeselectUnits() {
        selectedUnits.ToggleSelection(false);
        selectedUnits.Clear();
    }

    public void AssignFollowTask(){
        GameObject playerObj = GameObject.Find("Player");
        Player player = playerObj.GetComponent<Player>();
        player.units.AddUnits(selectedUnits.GetUnits());
        selectedUnits.Follow(playerObj, true);
    }

    public void AssignReturnTask() {
        //TODO: Use ReturnTask (currently only supports antibodies)
        //Temp use first index values start position
        selectedUnits.MoveTo(((MicroBacteria)selectedUnits.units[0]).startPosition, true);
    }

    public void AssignMoveTask(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        Transform hitTransform = hit.transform;
        Infectable infectableTarget = null;
        if(hitTransform != null){
            infectableTarget = hitTransform.gameObject.GetComponent<Infectable>();
        }
        if(infectableTarget != null){
            selectedUnits.Infect(infectableTarget, true);
        }
        else {  
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            selectedUnits.MoveTo(mousePosition, true);
        }

        GameObject playerObj = GameObject.Find("Player");
        Player player = playerObj.GetComponent<Player>();
        player.units.RemoveUnits(selectedUnits.GetUnits());      
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(selectedUnits.units.Count > 0){
                if(!controlPanel.activeSelf)
                    controlPanel.SetActive(true);
                if(Input.GetKeyDown(KeyCode.R)) // Return to factory
                    AssignReturnTask();
                else if(Input.GetKeyDown(KeyCode.F)) // Follow player
                    AssignFollowTask();
                else if(Input.GetMouseButtonDown(1)) // Move to mouse position
                    AssignMoveTask();
            }
            else {
                if(controlPanel.activeSelf)
                    controlPanel.SetActive(false);
            }

            if(!EventSystem.current.IsPointerOverGameObject()){
                if(Input.GetMouseButtonDown(0)){
                    selectionTransform.gameObject.SetActive(true);
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);
                    topLeft = mousePosition;
                    
                    isDown = true;
                }
                else if(Input.GetMouseButtonUp(0)){
                    selectionTransform.gameObject.SetActive(false);
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePosition = new Vector2(worldPosition.x, worldPosition.y);
                    bottomRight = mousePosition;

                    DeselectUnits();
                    SelectUnits();

                    isDown = false;
                }
                if(isDown){
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    
                    float xScale = Mathf.Abs(topLeft.x - worldPosition.x);
                    float yScale = Mathf.Abs(topLeft.y - worldPosition.y);

                    selectionTransform.localScale = new Vector2(xScale, yScale);
                    
                    //Draw selection box
                    if(worldPosition.x > topLeft.x && worldPosition.y < topLeft.y)
                        selectionTransform.position = new Vector2(worldPosition.x - xScale/2.0f, worldPosition.y + yScale/2.0f);
                    else if(worldPosition.x < topLeft.x && worldPosition.y > topLeft.y)
                        selectionTransform.position = new Vector2(worldPosition.x + xScale/2.0f, worldPosition.y - yScale/2.0f);
                    else if(worldPosition.x > topLeft.x && worldPosition.y > topLeft.y)
                        selectionTransform.position = new Vector2(worldPosition.x - xScale/2.0f, worldPosition.y - yScale/2.0f);
                    else
                        selectionTransform.position = new Vector2(worldPosition.x + xScale/2.0f, worldPosition.y + yScale/2.0f);
                }
            }
        }
    }
}
