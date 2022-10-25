using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Infectable
{
    public int upgradeCost = 100;
    public int currentLevel = 1;
    public int maxLevel = 4;
    public GameObject[] structures = new GameObject[4];

    public Infectable infectTarget;
    public GameObject cellPrefab;
    public List<GameObject> upgradeVisuals;
    float healCD = 8;
    float nextHeal = 0;

    public List<MicroBacteria> microbacterias = new List<MicroBacteria>();
    public List<MicroBacteria> bacteriaInside = new List<MicroBacteria>();
    
    public GameObject mouseObject;
    GameManager gm;
    void Start(){
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(GameObject.Find("mouse-tooltip").transform.GetChild(0))
            mouseObject = GameObject.Find("mouse-tooltip").transform.GetChild(0).gameObject;
    }

    public void MakeObjActive(GameObject obj){
        if(obj) {
            if(!obj.activeSelf) {
                obj.SetActive(true);
            }
        }
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            nextHeal -= Time.deltaTime;
            if(nextHeal < 0){
                nextHeal = healCD;
                if(bacteriaInside.Count > 0){
                    foreach(MicroBacteria bacteria in bacteriaInside){
                        bacteria.Heal(1);
                    }
                }
            }
            if(infectTarget)
                if(infectTarget.isInfected)
                    AutoInfect(null);           
        }
    }

    public int GetBacteriaAmount(){
        int amount = 0;
        foreach(GameObject structureObj in structures){
            if(structureObj != null){
                MicrobacteriaProducer bacteriaProducer = structureObj.GetComponent<MicrobacteriaProducer>();
                //Not all buildings are structure atm, not required.
                if(bacteriaProducer != null){
                    amount += bacteriaProducer.GetBacteriaAmount();
                }
            }
        }
        return amount;
    }

    public int GetMaximumBacteria(){
        int amount = 0;
        foreach(GameObject structureObj in structures){
            if(structureObj != null){
                MicrobacteriaProducer bacteriaProducer = structureObj.GetComponent<MicrobacteriaProducer>();
                //Not all buildings are structure atm, not required.
                if(bacteriaProducer != null){
                    amount += bacteriaProducer.GetMaximumAmount();
                }
            }
        }
        return amount;
    }
    
    public string GetAttackDirection(Vector2 playerPos){
        float upDownRange = 4.5f;
        float leftRightRange = 8.5f;

        if(transform.position.x < playerPos.x + leftRightRange && transform.position.x > playerPos.x - leftRightRange) {
            if(transform.position.y > playerPos.y)
                return "up";
            else
                return "down";
        }
        else if(transform.position.y < playerPos.y + upDownRange && transform.position.y > playerPos.y - upDownRange) {
            if(transform.position.x > playerPos.x)
                return "right";
            else
                return "left";
        }
        return "none";
    }

    [SerializeField] private GameObject warningPrefab;
    public override void OnGettingCured(){
        GameObject player = GameObject.Find("Player");
        Vector2 playerPos = player.transform.position;
        string attackDir = GetAttackDirection(playerPos);
        Vector2 pos = new Vector2();
        GameObject temp = null;
        switch(attackDir) {
            case "up":
                pos = new Vector2(transform.position.x, playerPos.y + 4.0f);
                temp = Instantiate(warningPrefab, pos, Quaternion.identity);
                break;
            case "down":
                pos = new Vector2(transform.position.x, playerPos.y - 4.0f);
                temp = Instantiate(warningPrefab, pos, Quaternion.identity);
                break;
            case "left":
                pos = new Vector2(playerPos.x - 8.0f, transform.position.y);
                temp = Instantiate(warningPrefab, pos, Quaternion.identity);
                break;
            case "right":
                pos = new Vector2(playerPos.x + 8.0f, transform.position.y);
                temp = Instantiate(warningPrefab, pos, Quaternion.identity);
                break;
        }
        if(temp != null){
            temp.transform.parent = player.transform;
        }
    }

    public override void OnCure(){
        gm.IncreaseInfectedCells(-1);
        gm.IncreaseFactoryAmount(-1);
        gm.timesCuredCell += 1;
        foreach(GameObject structure in structures){
            Destroy(structure);
        }
        GameObject cellObj = Instantiate(cellPrefab, transform.position, Quaternion.identity);

        Cell cell = cellObj.GetComponent<Cell>();
        cell.organ = organ;
        if(organ != null){
            organ.ReplaceCell(this, cell);
            cell.GetComponent<SpriteRenderer>().color = organ.GetOrganCellColor();
            GameObject.Find("GameManager").GetComponent<GameManager>().infectedHeartCells -= 1;
        }

        gm.ReplaceCell(gameObject, cellObj);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Player>().currentFactory = this;
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.MakeObjActive(playerScript.fObject);
        }
        MicroBacteria micro = col.GetComponent<MicroBacteria>();
        if(micro != null && !bacteriaInside.Contains(micro)){
            if(micro.stats.GetHealth() > micro.stats.GetCurrentHealth()){
                micro.healingIcon.SetActive(true);
            }
            bacteriaInside.Add(micro);
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<Player>().currentFactory = null;
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.MakeObjDeactive(playerScript.fObject);
        }
        MicroBacteria micro = col.GetComponent<MicroBacteria>();
        if(micro != null && bacteriaInside.Contains(micro)){
            micro.healingIcon.SetActive(false);
            bacteriaInside.Remove(micro);
        }        
    }

    public bool CanUpgrade(){
        bool levelsLeft = currentLevel < maxLevel;
        bool canAfford = GameObject.Find("GameManager").GetComponent<GameManager>().CanAffordSugar(upgradeCost);
        return levelsLeft && canAfford;
    }

    public bool CanBuildAt(int slot){
        return slot < currentLevel && structures[slot] == null;
    }

    public bool CanBuild(Buildable structure){
        bool canAfford = GameObject.Find("GameManager").GetComponent<GameManager>().CanAffordSugar(structure.cost);
        return canAfford;
    }

    public bool Upgrade(){
        bool canUpgrade = CanUpgrade();
        if(canUpgrade) {
            upgradeVisuals[currentLevel - 1].SetActive(true);
            currentLevel++;
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseSugar(-upgradeCost);
            upgradeCost *= 2;
        }
        return canUpgrade;
    }

    public void AutoInfect(Infectable newTarget){
        infectTarget = newTarget;
        if(newTarget != null){
            foreach(MicroBacteria bacteria in microbacterias){
                bacteria.CancelTasks();
                bacteria.GiveTask(new InfectTask(bacteria, newTarget.gameObject), false);
            }
        }
    }

    public void JoinPlayerSquad(GameObject playerObject, UnitSquad unitSquad){
        foreach (Unit microbacteria in microbacterias){
            unitSquad.AddUnit(microbacteria);
        }
        unitSquad.Follow(playerObject, true);
    }

    public void AddMicrobacteria(MicroBacteria bacteria){
        microbacterias.Add(bacteria);
    }

    public void Build(int slot, Buildable structure){
        Vector2 randomPos = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        randomPos.Normalize();
        GameObject temp = Instantiate(structure.structure, new Vector3(transform.position.x + randomPos.x, transform.position.y + randomPos.y, -2f), Quaternion.identity);
        structures[slot] = temp;
        Structure newStructure = temp.GetComponent<Structure>();
        //Not all buildings are structure atm, not required.
        if(newStructure != null){
            newStructure.builtBy = this;
        }
    }
}
