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

    private GameObject selectionMenu;

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

    public void SetTargetAndReset(Cell targetCell) {
        foreach (MicroBacteria bacteria in selectedMicroBacterias) {
            bacteria.targetCell = targetCell;
        }
        Destroy(selectionMenu);
        isTargeting = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().isPaused = false;
        selectedMicroBacterias = new List<MicroBacteria>();
    }

    void Update()
    {
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

            selectedMicroBacterias = GetSelectedBacterias();
            foreach (MicroBacteria bacteria in selectedMicroBacterias) {
                bacteria.ToggleSelection();
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
        else {
            if(!isTargeting) {
                if(selectedMicroBacterias.Count >= 1) {
                    isTargeting = true;
                    GameObject.Find("Player").GetComponent<PlayerMovement>().isPaused = true;
                    GameObject player = GameObject.Find("Player");
                    GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
                    selectionMenu = Instantiate(windowPrefab, new Vector3(player.transform.position.x, player.transform.position.y, -5.0f), Quaternion.identity);
                    for(int i = 0; i < objs.Length; i++) {
                        if(Vector2.Distance(objs[i].transform.position, player.transform.position) <= targetCellRadius) {
                            Debug.Log("Found Cell!");
                            Vector2 objPos = objs[i].transform.position;

                            float diffInX = objPos.x - player.transform.position.x;
                            float diffInY = objPos.y - player.transform.position.y;
                            GameObject fakeCell = Instantiate(fakeCellPrefab, new Vector3(player.transform.position.x + diffInX/5.0f, player.transform.position.y + diffInY/5.0f, -6.0f), Quaternion.identity);
                            
                            fakeCell.transform.parent = selectionMenu.transform;
                            fakeCell.GetComponent<FakeCell>().realCellReference = objs[i].GetComponent<Cell>();
                        }
                    }
                }
            }
        }
    }
}
