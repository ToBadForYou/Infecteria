using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCell : MonoBehaviour
{
    public Factory currentFactory;

    public float targetCellRadius;

    private GameObject selectionMenu;

    public GameObject newSelectionCell;
    public GameObject newSelectionMenu;

    public void SetTargetCell(Cell targetCell){
        currentFactory.AutoInfect(targetCell);
        CloseSelection();
    }

    public void CloseSelection(){
        newSelectionMenu.SetActive(false);
        currentFactory = null;
        PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
    }

    public void ViewNearbyCells(Factory factory){
        currentFactory = factory;
        GameObject player = GameObject.Find("Player");

        PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        
        newSelectionMenu.SetActive(true);

        for(int i = 0; i < objs.Length; i++) {
            if(Vector2.Distance(objs[i].transform.position, player.transform.position) <= targetCellRadius) {
                Vector2 objPos = objs[i].transform.position;

                float diffInX = objPos.x - player.transform.position.x;
                float diffInY = objPos.y - player.transform.position.y;

                GameObject fakeCell = Instantiate(newSelectionCell);
                RectTransform myRectTransform = fakeCell.GetComponent<RectTransform>();
                myRectTransform.localPosition = new Vector2(diffInX*10.0f, diffInY*10.0f);
                myRectTransform.SetParent(newSelectionMenu.transform, false);

                fakeCell.GetComponent<SelectionCell>().realCellReference = objs[i].GetComponent<Cell>();
            }
        }
    }

    void Update(){
        if(newSelectionMenu)
            if(Input.GetKeyDown(KeyCode.Escape))
                CloseSelection();
    }

}
