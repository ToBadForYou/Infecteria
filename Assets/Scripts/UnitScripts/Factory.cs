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
    public GameObject cellPrefab;
    public List<GameObject> upgradeVisuals;

    public List<MicroBacteria> microbacterias = new List<MicroBacteria>();

    void Update(){
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

    public override void OnCure(){
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.IncreaseInfectedCells(-1);
        gm.IncreaseFactoryAmount(-1);

        GameObject cell = Instantiate(cellPrefab, transform.position, Quaternion.identity);
        gm.ReplaceCell(gameObject, cell);      
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(2.5f);
            col.gameObject.GetComponent<Player>().currentFactory = this;
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.MakeObjActive(playerScript.fObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerMovement>().SetSpeed(5.0f);
            col.gameObject.GetComponent<Player>().currentFactory = null;
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.MakeObjDeactive(playerScript.fObject);
        }
    }

    public bool CanUpgrade(){
        bool levelsLeft = currentLevel < maxLevel;
        bool hasPoints = GameObject.Find("GameManager").GetComponent<GameManager>().sugar >= upgradeCost;
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
            upgradeVisuals[currentLevel - 1].SetActive(true);
            currentLevel++;
            GameObject.Find("GameManager").GetComponent<GameManager>().DNAPoints -= upgradeCost;
            upgradeCost *= 2;
        }
    }

    public void AutoInfect(Infectable newTarget) {
        infectTarget = newTarget;
        foreach(MicroBacteria bacteria in microbacterias){
            bacteria.CancelTasks();
            bacteria.GiveTask(new InfectTask(bacteria, newTarget.gameObject));
        }
        autoInfect = true;
    }

    public void JoinPlayerSquad(GameObject playerObject, UnitSquad unitSquad){
        foreach (Unit microbacteria in microbacterias){
            unitSquad.AddUnit(microbacteria);
        }
        unitSquad.Follow(playerObject, true);
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
