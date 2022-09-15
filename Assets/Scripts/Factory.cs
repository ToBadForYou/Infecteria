using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Infectable
{
    public GameObject microBacteriaPrefab;
    public int bacteriaAmount = 0;
    public int maxBacteriaAmount = 10;

    public float startTime = 10.0f;
    public float time = 10.0f;
    
    public int currentLevel = 1;
    public int maxLevel = 4;
    public GameObject[] structures;

    public bool autoInfect = false;
    public Cell targetCell;

    public List<MicroBacteria> microbacterias = new List<MicroBacteria>();

    void Start(){
        structures = new GameObject[4];
    }

    void Update()
    {
        if(bacteriaAmount < maxBacteriaAmount) {
            time -= Time.deltaTime;
            if(time <= 0.0f) {
                GameObject obj = Instantiate(microBacteriaPrefab, transform.position, Quaternion.identity);
                obj.GetComponent<MicroBacteria>().unitMovement.MoveToPosition(new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f)));
                MicroBacteria bacteria = obj.GetComponent<MicroBacteria>();
                AddMicrobacteria(bacteria);
                if(autoInfect) {
                    bacteria.isSelected = true;
                    bacteria.targetCell = targetCell;
                }
                time = startTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(2.5f);
            col.gameObject.GetComponent<Player>().currentFactory = this;
            Player playerScript = col.gameObject.GetComponent<Player>();
            if(CanAutoInfect()) {
                playerScript.MakeObjActive(playerScript.iObject);
            }
            playerScript.MakeObjActive(playerScript.fObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(5.0f);
            col.gameObject.GetComponent<Player>().currentFactory = null;
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.MakeObjDeactive(playerScript.iObject);
            playerScript.MakeObjDeactive(playerScript.fObject);
        }
    }

    public bool CanAutoInfect() {
        return !autoInfect;
    }

    public bool CanBuild(int slot) {
        return slot < currentLevel && structures[slot] == null;
    }

    public void AutoInfect() {
        targetCell = GameObject.Find("Cell(Clone)").GetComponent<Cell>(); //Temporarily just get random cell

        foreach(MicroBacteria bacteria in microbacterias) {
            bacteria.isSelected = true;
            bacteria.targetCell = targetCell;
            bacteria.followPlayer = false;
        }

        autoInfect = true;
    }

    public void SendBacteriasToPlayer() {
        autoInfect = false;
        targetCell = null;
        foreach(MicroBacteria bacteria in microbacterias) {
            bacteria.followPlayer = true;
        }
    }

    public void AddMicrobacteria(MicroBacteria bacteria) {
        bacteria.parent = this;
        microbacterias.Add(bacteria);
        bacteriaAmount++;
    }

    public void RemoveMicrobacteria(MicroBacteria bacteria) {
        microbacterias.Remove(bacteria);
        bacteriaAmount--;
    }

    public void Build(int slot, Buildable structure) {
        GameObject temp = Instantiate(structure.structure, new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f), -2.0f), Quaternion.identity);
        temp.transform.parent = transform;
        structures[slot] = temp;
    }
}
