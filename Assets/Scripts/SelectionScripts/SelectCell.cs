using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCell : MonoBehaviour
{
    public Factory currentFactory;

    public float targetCellRadius;

    private GameObject selectionMenu;

    public GameObject newSelectionCell;
    public GameObject newSelectionMenu;
    SelectionCell selectedCell;
    public List<GameObject> fakeCells = new List<GameObject>();

    public void SetTargetCell(SelectionCell targetFakeCell){
        if(selectedCell == targetFakeCell){
            targetFakeCell.infectText.text = "";
            selectedCell = null;
            currentFactory.AutoInfect(null);
        }
        else {
            if(selectedCell != null){
                selectedCell.infectText.text = "";
            }
            selectedCell = targetFakeCell;
            targetFakeCell.infectText.text = "Infecting";
            currentFactory.AutoInfect(targetFakeCell.realCellReference);
        }
    }

    public void CloseSelection(){
        newSelectionMenu.SetActive(false);
        foreach (GameObject fakeCell in fakeCells){
            Destroy(fakeCell);
        }
        fakeCells.Clear();
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
                Cell cell = objs[i].GetComponent<Cell>();
                float infectAmount = cell.infectionAmount/10.0f;
                fakeCell.GetComponent<Image>().color = new Color(1 - infectAmount, 1, 1 - infectAmount, 1);
                
                SelectionCell selectionCell = fakeCell.GetComponent<SelectionCell>();
                selectionCell.realCellReference = cell;
                if(factory.infectTarget != null && factory.infectTarget.gameObject == objs[i]){
                    selectionCell.infectText.text = "Infecting";
                    selectedCell = selectionCell;
                }
                fakeCells.Add(fakeCell);
            }
        }
    }

    void Update(){
        if(newSelectionMenu)
            if(Input.GetKeyDown(KeyCode.Escape))
                CloseSelection();
    }

}
