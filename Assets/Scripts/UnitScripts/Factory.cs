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
    
    public int upgradeCost = 100;
    public int currentLevel = 1;
    public int maxLevel = 4;
    public GameObject[] structures = new GameObject[4];

    public bool autoInfect = false;
    public Infectable infectTarget;

    public List<MicroBacteria> microbacterias = new List<MicroBacteria>();

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
                    bacteria.GiveTask(new InfectTask(bacteria, infectTarget.gameObject));
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

    public bool CanUpgrade() {
        bool levelsLeft = currentLevel < maxLevel;
        bool hasPoints = GameObject.Find("GameManager").GetComponent<GameManager>().DNAPoints >= upgradeCost;
        return levelsLeft && hasPoints;
    }

    public bool CanAutoInfect() {
        return !autoInfect;
    }

    public bool CanBuild(int slot) {
        return slot < currentLevel && structures[slot] == null;
    }

    public void Upgrade() {
        if(CanUpgrade()) {
            currentLevel++;
            GameObject.Find("GameManager").GetComponent<GameManager>().DNAPoints -= upgradeCost;
            upgradeCost *= 2;
        }
    }

    public void AutoInfect(Infectable newTarget) {
        infectTarget = newTarget;
        foreach(MicroBacteria bacteria in microbacterias){
            bacteria.GiveTask(new InfectTask(bacteria, newTarget.gameObject));
        }

        autoInfect = true;
    }

    public void JoinPlayerSquad(GameObject playerObject, UnitSquad unitSquad){
        foreach(Unit bacteria in microbacterias) {
            unitSquad.AddUnit(bacteria);
            bacteria.GiveTask(new FollowTask(bacteria, playerObject));
        }        
    }

    public void SendBacteriasToPlayer() {
        autoInfect = false;
        infectTarget = null;
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
