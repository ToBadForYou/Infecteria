using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCell : MonoBehaviour
{
    public GameObject fakeCellPrefab;
    public GameObject windowPrefab;
    public Factory currentFactory;

    public float targetCellRadius;

    private GameObject selectionMenu;

    public void SetTargetCell(Cell targetCell){
        currentFactory.AutoInfect(targetCell);
        CloseSelection();
    }

    public void CloseSelection(){
        Destroy(selectionMenu);
        currentFactory = null;
        GameObject.Find("Player").GetComponent<PlayerMovement>().isPaused = false;
    }

    public void ViewNearbyCells(Factory factory){
        currentFactory = factory;
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().isPaused = true;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        selectionMenu = Instantiate(windowPrefab, new Vector3(player.transform.position.x, player.transform.position.y, -5.0f), Quaternion.identity);
        for(int i = 0; i < objs.Length; i++) {
            if(Vector2.Distance(objs[i].transform.position, player.transform.position) <= targetCellRadius) {
                Vector2 objPos = objs[i].transform.position;

                float diffInX = objPos.x - player.transform.position.x;
                float diffInY = objPos.y - player.transform.position.y;
                GameObject fakeCell = Instantiate(fakeCellPrefab, new Vector3(player.transform.position.x + diffInX/5.0f, player.transform.position.y + diffInY/5.0f, -6.0f), Quaternion.identity);
                
                fakeCell.transform.parent = selectionMenu.transform;
                fakeCell.GetComponent<FakeCell>().realCellReference = objs[i].GetComponent<Cell>();
            }
        }
    }

    void Update(){
        if(selectionMenu) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                CloseSelection();
            }
        }
    }

}
