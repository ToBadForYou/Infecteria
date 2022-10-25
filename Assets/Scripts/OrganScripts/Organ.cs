using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Organ : MonoBehaviour
{
    public List<UnitSquad> unitSquads;
    public List<Infectable> cells = new List<Infectable>();
    public UnitSpawner unitSpawner;
    public UnitProducer unitProducer;
    float yRange = 13.0f;
    float xRange = 16.0f;
    public float nextInspection;
    public int inspectionCD = 30;
    public Unit.Faction owner = Unit.Faction.IMMUNESYSTEM;
    public int minimumSpawn = 5;
    public int countOrganCells;

    protected void Start(){
        nextInspection = inspectionCD;
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        unitProducer.AddProduction(UnitType.ANTIBODY, new UnitProductionData(30, 30, 10));
        StartCoroutine(LateStart(0.0f));
    }

    protected void Update(){
        nextInspection -= Time.deltaTime;
        if(nextInspection < 0){
            nextInspection = inspectionCD;
            AttemptCureInfectedCells();
        }
    }

    void AttemptCureInfectedCells(){
        foreach(Infectable cell in cells){
            if(cell.isInfected){
                int currentAntibodies = unitProducer.GetAvailableAmount(UnitType.ANTIBODY);
                if(currentAntibodies >= 2){
                    currentAntibodies = unitProducer.WithdrawAmount(UnitType.ANTIBODY, 2);
                    List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
                    UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
                    unitSquads.Add(newSquad);
                    newSquad.Infect(cell, true);                  
                }
            }
        }
    }

    public void RemoveCell(Infectable cell){
        if(cells.Contains(cell)) {
            cells.Remove(cell);
        }
        CheckVictory();
    }

    public void ReplaceCell(Infectable oldCell, Infectable replacement) {
        int index = cells.FindIndex(cell => cell.gameObject == oldCell.gameObject);
        cells[index] = replacement;
        CheckVictory();
    }

    public void CheckVictory(){
        bool allInfected = true;
        foreach (Infectable cell in cells){
            if(!cell.isInfected){
                allInfected = false;
                break;
            }
        }
        if(allInfected){
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

    public Color GetOrganCellColor(){
        Color organColor = gameObject.GetComponent<SpriteRenderer>().color;
        return new Color(organColor.r + 0.1f, organColor.g + 0.1f, organColor.b + 0.1f, organColor.a);
    }

    void FillWithNearbyCells() {
        Color cellColor = GetOrganCellColor();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        for(int i = 0; i < objs.Length; i++) {
            float xDiff = Mathf.Abs(objs[i].transform.position.x - transform.position.x);
            float yDiff = Mathf.Abs(objs[i].transform.position.y - transform.position.y);
            if(xDiff <= xRange && yDiff <= yRange) {
                objs[i].GetComponent<Cell>().organ = this;
                cells.Add(objs[i].GetComponent<Cell>());
                objs[i].GetComponent<SpriteRenderer>().color = cellColor;
                //objs[i].transform.Find("cell-inside").GetComponent<SpriteRenderer>().color = innerColor;
            }
        }
        countOrganCells = cells.Count;
    }

    protected void DeployAntibodies(Vector2 alertPosition){
        int currentAntibodies = unitProducer.GetAvailableAmount(UnitType.ANTIBODY);
        if(currentAntibodies > minimumSpawn){
            currentAntibodies = unitProducer.WithdrawAll(UnitType.ANTIBODY);
            // TODO Recall all antibodies
            List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, currentAntibodies);
            UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
            unitSquads.Add(newSquad);
            newSquad.MoveTo(alertPosition, false);
            foreach (Unit antibody in antibodies){
                antibody.producer = unitProducer;
                antibody.GiveTask(new ReturnTask(antibody, gameObject), false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        Unit unit = col.gameObject.GetComponent<Unit>();
        if(unit != null && unit.owner != owner){
            DeployAntibodies(col.transform.position);
        }
    }
}
