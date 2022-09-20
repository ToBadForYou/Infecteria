using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organ : MonoBehaviour
{
    public List<UnitSquad> unitSquads;
    public List<Cell> cells = new List<Cell>();
    public UnitSpawner unitSpawner;
    float yRange = 13.0f;
    float xRange = 16.0f;

    void Start(){
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        StartCoroutine(LateStart(0.0f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FillWithNearbyCells();
    }

    void FillWithNearbyCells() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Cell");
        for(int i = 0; i < objs.Length; i++) {
            float xDiff = Mathf.Abs(objs[i].transform.position.x - transform.position.x);
            float yDiff = Mathf.Abs(objs[i].transform.position.y - transform.position.y);
            if(xDiff <= xRange && yDiff <= yRange) {
                cells.Add(objs[i].GetComponent<Cell>());
                objs[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.0f, 0.0f, 1.0f);
            }
        }
    }
}
