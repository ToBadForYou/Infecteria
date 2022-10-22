using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Infectable
{
    public GameObject microBacteriaPrefab;
    public UnitProducer unitProducer;
    
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

    GameManager gm;
    void Start(){
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        unitProducer.AddProduction(UnitType.MICROBACTERIA, new UnitProductionData(0, 0, 15));
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            int withdrawAmount = unitProducer.WithdrawAmount(UnitType.MICROBACTERIA, 1);
            if(withdrawAmount > 0) {
                gm.producedMicrobacterias++;
                GameObject obj = Instantiate(microBacteriaPrefab, transform.position, Quaternion.identity);
                obj.GetComponent<MicroBacteria>().unitMovement.MoveToPosition(new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f)));
                MicroBacteria bacteria = obj.GetComponent<MicroBacteria>();
                AddMicrobacteria(bacteria);
                if(infectTarget != null){
                    bacteria.GiveTask(new InfectTask(bacteria, infectTarget.gameObject), false);
                }
            }
            nextHeal -= Time.deltaTime;
            if(nextHeal < 0){
                nextHeal = healCD;
                if(bacteriaInside.Count > 0){
                    foreach(MicroBacteria bacteria in bacteriaInside){
                        bacteria.Heal(1);
                    }
                }
            }           
        }
    }

    public int GetBacteriaAmount(){
        return unitProducer.GetSpawnedAmount(UnitType.MICROBACTERIA);
    }

    public int GetMaximumBacteria(){
        return unitProducer.GetMaximumAmount(UnitType.MICROBACTERIA);
    }
    
    public string GetAttackDirection() {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        
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
    public override void OnGettingCured() {
        string attackDir = GetAttackDirection();
        GameObject player = GameObject.Find("Player");
        Vector2 playerPos = player.transform.position;
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
        temp.transform.parent = player.transform;
    }

    public override void OnCure(){
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.IncreaseInfectedCells(-1);
        gm.IncreaseFactoryAmount(-1);
        foreach(GameObject structure in structures){
            Destroy(structure);
        }
        GameObject cellObj = Instantiate(cellPrefab, transform.position, Quaternion.identity);

        Cell cell = cellObj.GetComponent<Cell>();
        cell.organ = organ;
        if(organ != null)
            organ.ReplaceCell(this, cell);
            cell.GetComponent<SpriteRenderer>().color = organ.GetOrganCellColor();

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
        bool hasPoints = GameObject.Find("GameManager").GetComponent<GameManager>().GetSugarAmount() >= upgradeCost;
        return levelsLeft && hasPoints;
    }

    public bool CanBuild(int slot){
        return slot < currentLevel && structures[slot] == null;
    }

    public void Upgrade(){
        if(CanUpgrade()) {
            upgradeVisuals[currentLevel - 1].SetActive(true);
            currentLevel++;
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseSugar(-upgradeCost);
            upgradeCost *= 2;
        }
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
        bacteria.producer = unitProducer;
        microbacterias.Add(bacteria);
    }

    public void Build(int slot, Buildable structure){
        GameObject temp = Instantiate(structure.structure, new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f), -2.0f), Quaternion.identity);
        structures[slot] = temp;
        unitProducer.IncreaseMaximumUnit(UnitType.MICROBACTERIA, structure.microbacteriaProduction);
    }
}
