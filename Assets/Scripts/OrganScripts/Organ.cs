using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Organ : MonoBehaviour
{
    public List<UnitSquad> unitSquads;
    public List<Cell> cells = new List<Cell>();
    public UnitSpawner unitSpawner;
    float yRange = 13.0f;
    float xRange = 16.0f;
    public int maxStoredAntibodies = 50;
    public int currentAntibodies = 20;
    int antibodyProductionTime = 20;
    float nextAntibody = 0;

    void Start(){
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        StartCoroutine(LateStart(0.0f));
    }

    protected void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            nextAntibody -= Time.deltaTime;
            if(currentAntibodies < maxStoredAntibodies && nextAntibody < 0){
                nextAntibody = antibodyProductionTime;
                currentAntibodies += 1;
            }
        }
    }

    public void RemoveCell(Cell cell) {
        if(cells.Contains(cell)) {
            cells.Remove(cell);
        }
        if(cells.Count == 0) {
            DontDestroyOnLoad(GameObject.Find("GameManager"));
            SceneManager.LoadScene("Ending");
        }
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FillWithNearbyCells();
    }

    void FillWithNearbyCells() {
        Color organColor = gameObject.GetComponent<SpriteRenderer>().color;
        Color cellColor = new Color(organColor.r + 0.1f, organColor.g + 0.1f, organColor.b + 0.1f, organColor.a);
        //Color innerColor = new Color(organColor.r * 1.5f, organColor.g * 1.5f, organColor.b * 1.5f, organColor.a);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        for(int i = 0; i < objs.Length; i++) {
            float xDiff = Mathf.Abs(objs[i].transform.position.x - transform.position.x);
            float yDiff = Mathf.Abs(objs[i].transform.position.y - transform.position.y);
            if(xDiff <= xRange && yDiff <= yRange) {
                objs[i].GetComponent<Cell>().belongsToHeart = true;
                cells.Add(objs[i].GetComponent<Cell>());
                objs[i].GetComponent<SpriteRenderer>().color = cellColor;
                //objs[i].transform.Find("cell-inside").GetComponent<SpriteRenderer>().color = innerColor;
            }
        }
    }

    public virtual void OnUnitReturn(Unit returnUnit){
        // TODO Add support for multiple types of units
        // TODO Use for scouts returning
        currentAntibodies += 1;
        Destroy(returnUnit.transform.root.gameObject);
    }
}
