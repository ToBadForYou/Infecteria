using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public List<GameObject> cells = new List<GameObject>();
    public GameObject scoutObject;
    public GameObject antibodyObject;
    public float scoutSpawnTimer = 5;
    public int baseScoutSpawnTime = 5;
    public int maxAntibodies = 6;
    public List<Cell> heartCells = new List<Cell>();
    float yRange = 16.0f;
    float xRange = 13.0f;

    void Start()
    {
        StartCoroutine(LateStart(1.0f));
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
            SpawnScout();
        }        
    }

    void SpawnScout(){
        GameObject newScout = Instantiate(scoutObject, transform.position, Quaternion.identity);
        GameObject randomTarget = cells[Random.Range(0, cells.Count)];
        Scout scout = newScout.transform.Find("scoutRange").gameObject.GetComponent<Scout>();
        scout.parent = gameObject;
        scout.SetTarget(randomTarget);
    }

    void SpawnAntibodies(GameObject alertObject){
        int antibodiesAmount = Random.Range(2, maxAntibodies);
        for (int i = 0; i < antibodiesAmount; i++)
        {
            GameObject newAntibody = Instantiate(antibodyObject, new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            newAntibody.GetComponent<Unit>().unitMovement.MoveToTarget(alertObject);
        }
    }

    public void OnReport(GameObject alertObject){
        SpawnAntibodies(alertObject);
    }

    public void AddCell(GameObject cell){
        cells.Add(cell);
    }
}
