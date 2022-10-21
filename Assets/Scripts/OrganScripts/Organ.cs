using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Organ : MonoBehaviour
{
    public List<UnitSquad> unitSquads;
    public List<Cell> cells = new List<Cell>();
    public UnitSpawner unitSpawner;
    public UnitProducer unitProducer;
    float yRange = 13.0f;
    float xRange = 16.0f;
    public float nextInspection;
    public int inspectionCD = 120;

    protected void Start(){
        nextInspection = inspectionCD;
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        unitProducer.AddProduction(UnitType.ANTIBODY, new UnitProductionData(20, 50, 20));
        StartCoroutine(LateStart(0.0f));
    }

    protected void Update(){
        nextInspection -= Time.deltaTime;
        # TODO actually check cells
        if(nextInspection < 0){
            nextInspection = inspectionCD;
        }
    }

    public void RemoveCell(Cell cell) {
        if(cells.Contains(cell)) {
            cells.Remove(cell);
        }
        if(cells.Count == 0) {
            GameObject manager = GameObject.Find("GameManager");
            manager.GetComponent<GameManager>().won = true;
            DontDestroyOnLoad(manager);
            SceneManager.LoadScene("Ending");
        }
    }

    IEnumerator LateStart(float waitTime){
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
}
