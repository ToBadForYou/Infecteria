using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public List<GameObject> cells = new List<GameObject>();
    public GameObject scoutObject;
    public float scoutSpawnTimer = 5;
    public int baseScoutSpawnTime = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        Transform randomTarget = cells[Random.Range(0, cells.Count)].GetComponent<Transform>();
        newScout.GetComponent<Scout>().SetTarget(randomTarget);
    }

    public void AddCell(GameObject cell){
        cells.Add(cell);
    }
}
