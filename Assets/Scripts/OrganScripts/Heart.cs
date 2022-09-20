using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public List<UnitSquad> unitSquads;
    public float scoutSpawnTimer = 5;
    public int baseScoutSpawnTime = 5;
    public int maxAntibodies = 6;
    public List<Cell> heartCells = new List<Cell>();
    float yRange = 16.0f;
    float xRange = 13.0f;
    UnitSpawner unitSpawner;

    void Start()
    {
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        StartCoroutine(LateStart(0.0f));
    }
 
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FillWithNearbyCells();
    }

    public void FillWithNearbyCells() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        for(int i = 0; i < objs.Length; i++) {
            float xDiff = Mathf.Abs(objs[i].transform.position.x - transform.position.x);
            float yDiff = Mathf.Abs(objs[i].transform.position.y - transform.position.y);
            if(xDiff <= xRange && yDiff <= yRange) {
                heartCells.Add(objs[i].GetComponent<Cell>());
                objs[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.0f, 0.0f, 1.0f);
            }
        }
    }

    void Update()
    {
        scoutSpawnTimer -= Time.deltaTime;
        if (scoutSpawnTimer < 0){
            scoutSpawnTimer = baseScoutSpawnTime;
            Scout newScout = unitSpawner.SpawnScout(transform.position, gameObject);
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            newScout.SetTarget(gm.cells[Random.Range(0, gm.cells.Count)]);
        }        
    }

    UnitSquad CreateSquad(List<Unit> units){
        GameObject tempObj = new GameObject("UnitSquad");
        UnitSquad newUnitSquad = tempObj.AddComponent<UnitSquad>();
        newUnitSquad.AddUnits(units);
        unitSquads.Add(newUnitSquad);
        return newUnitSquad;
    }

    public void OnReport(Vector2 alertPosition){
        List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, Random.Range(2, maxAntibodies));
        UnitSquad newSquad = CreateSquad(antibodies);
        newSquad.MoveTo(alertPosition);
        newSquad.Search(alertPosition);
    }
}
