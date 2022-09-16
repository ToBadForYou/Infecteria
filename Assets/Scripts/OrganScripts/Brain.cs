using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    public List<Cell> brainCells = new List<Cell>();
    float yRange = 13.0f;
    float xRange = 16.0f;

    void Start()
    {
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
                brainCells.Add(objs[i].GetComponent<Cell>());
                objs[i].GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.3f, 0.3f, 1.0f);
            }
        }
    }
}
