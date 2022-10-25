using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{   
    public enum Faction { BACTERIA, IMMUNESYSTEM, NEUTRAL };
    public Transform healthBar;
    public UnitMovement unitMovement;
    public List<GameObject> inRange;
    public UnitSquad squad;
    public UnitStats stats;
    public UnitType unitType;
    public UnitProducer producer;

    public Faction owner;
    public List<Task> currentTasks = new List<Task>();
    public Task proximityHostile;
    public GameObject hitEffect;
    public bool isSelected;
    public Vector2 hostileDetectionPos;
    public float maxHPBar;

    public SoundManager deathSoundManager;
    public SoundManager hitSoundManager;

    GameObject healthBarBackground = null;
    public GameObject healingIcon;
    public SpriteRenderer stanceIcon;
    
    public Sprite aggressive;
    public Sprite defensive;
    bool aggressiveStance = false;

    int nextProximityCheck = 0;

    int startSortingOrder;
    SpriteRenderer sr;
    protected void Start(){
        sr = GetComponent<SpriteRenderer>();
        if(sr)
            startSortingOrder = sr.sortingOrder; 

        maxHPBar = healthBar.localScale.x;
        if(unitMovement != null){
            for(int i = 0; i < healthBar.parent.childCount; i++) {
                if(healthBar.parent.GetChild(i).gameObject.name == "healthBarBackground")
                    healthBarBackground = healthBar.parent.GetChild(i).gameObject;
            }

            if(healthBarBackground){
                healthBar.gameObject.SetActive(false);
                healthBarBackground.SetActive(false);
            }
        }
    }

    public void SetUnitStats(int hp, int currentHp, int dmg, float speed, float time, float r, bool state) {
        stats = new UnitStats(hp, currentHp, dmg, speed, time, r, state);
    }

    float sortingOrderTimer = 2.0f;
    float sortingOrderMaxTime = 2.0f;
    protected void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if (stats.IsAggressive()){
                if(!CanAttack())
                    stats.DecreaseAttackTimer(Time.deltaTime);
                if(nextProximityCheck < 1){
                    nextProximityCheck = 2;
                    if(!IsReturning() || proximityHostile == null || proximityHostile.target == null || Vector2.Distance(transform.position, proximityHostile.target.transform.position) > stats.GetRange() + 0.1f){
                        FindHostileInProximity();
                    }
                }
                else {
                    nextProximityCheck -= 1;
                }    
            }
            UpdateCurrentTask();

            if(sr) {
                if(sr.sortingOrder != startSortingOrder) {
                    sortingOrderTimer -= Time.deltaTime;
                    if(sortingOrderTimer <= 0.0f) {
                        sortingOrderTimer = sortingOrderMaxTime;
                        sr.sortingOrder = startSortingOrder;
                    }
                }
            }
        }
    }

    public void SetStance(bool setAggressive){
        if(setAggressive != aggressiveStance){
            if(setAggressive){
                stanceIcon.sprite = aggressive;
            }
            else {
                stanceIcon.sprite = defensive;
            }
            aggressiveStance = setAggressive;
        }
    }

    public TaskType GetTaskType(){
        TaskType currentTaskType = TaskType.IDLE;
        if(currentTasks.Count > 0){
            currentTaskType = currentTasks[0].taskType;
        }
        return currentTaskType;
    }

    public bool IsReturning(){
        return GetTaskType() == TaskType.RETURN;
    }

    public void CancelTasks(){
        currentTasks.Clear();
    }

    public void FindHostileInProximity(){
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject closeObject in inRange){
            if(IsHostile(closeObject.GetComponent<Unit>())){
                float distance = Vector2.Distance(transform.position, closeObject.transform.position);
                if(distance <= closestDistance) {
                    closest = closeObject;
                    closestDistance = distance;
                }
            }
        }

        if (closest != null && (proximityHostile == null || closest != proximityHostile.GetTarget())){
            if(proximityHostile == null){
                hostileDetectionPos = transform.position;
            }
            proximityHostile = new AttackTask(this, closest);
            //if(squad != null){
              //  squad.HostileDetected(closest);
            //}
        }       
    }

    public void UpdateCurrentTask(){
        Task currentTask = null;
        if(IsReturning()){
            proximityHostile = null;
        }
        if(proximityHostile != null){
            //Temp fix for not chasing units forever
            if(!aggressiveStance && Vector2.Distance(transform.position, hostileDetectionPos) > 10){
                proximityHostile = null;
                GiveTask(new MoveTask(this, hostileDetectionPos), true);
            }
            else {
                currentTask = proximityHostile;
            }
        }
        else if(currentTasks.Count > 0){
            currentTask = currentTasks[0];
        }
        if(currentTask != null){
            if(currentTask.finished){
                if(currentTask == proximityHostile){
                    proximityHostile = null;
                }
                else {
                    currentTasks.Remove(currentTask);
                }
            } else {
                currentTask.Update();
            }
        }
    }

    public void GiveTask(Task task, bool priority){
        if(priority){
            currentTasks.Insert(0, task);
        }
        else {
            currentTasks.Add(task);
        }
    }

    public bool InRange(Vector2 position){
        return Vector2.Distance(transform.position, position) <= stats.GetRange();
    }

    public bool IsHostile(Unit unit){
        return unit.owner != Faction.NEUTRAL && owner != unit.owner;
    }

    public bool CanAttack(){
        return stats.GetAttackTimer() <= 0;
    }

    public virtual bool AttackTarget(GameObject attackTarget){
        if(CanAttack()){
            stats.SetAttackTimer(stats.GetAttackSpeed());
            Unit unit = attackTarget.GetComponent<Unit>();
            return unit.TakeDamage(stats.GetDamage());
        }
        return false;
    }

    public void Heal(int amount){
        int maxHP = stats.GetHealth();
        int currentHP = stats.GetCurrentHealth();
        if(currentHP < maxHP){
            int newHP = currentHP + amount;
            if(newHP > maxHP){
                newHP = maxHP;
            }
            stats.SetCurrentHealth(newHP);
            healthBar.localScale = new Vector2(((float)newHP/maxHP) * maxHPBar, healthBar.localScale.y);
            if(newHP == maxHP && healthBarBackground){
                healthBar.gameObject.SetActive(false);
                healthBarBackground.SetActive(false);
                healingIcon.SetActive(false);
            }
        }
    }

    public bool TakeDamage(int takenDamage){
        stats.DecreaseCurrentHealth(takenDamage);
        OnTakeDamage();

        if(!healthBar.gameObject.activeSelf) { // Hide healthbar before taken hit
            healthBar.gameObject.SetActive(true);
            healthBarBackground.SetActive(true);
        }

        if(sr)
            sr.sortingOrder = startSortingOrder + 1;

        healthBar.localScale = new Vector2(((float)stats.GetCurrentHealth()/stats.GetHealth()) * maxHPBar, healthBar.localScale.y);
        if (stats.GetCurrentHealth() <= 0){
            OnDeath();
            Destroy(gameObject.transform.root.gameObject);
            return true;
        } 
        return false;
    }

    public virtual void OnTakeDamage(){
        hitSoundManager.CreateAudioSrc();
        hitSoundManager.PlaySound();
        GetComponent<DamageEffect>().Activate();
    }

    public virtual void OnDeath(){
        deathSoundManager.CreateAudioSrc();
        deathSoundManager.PlaySound();
        if(producer != null){
            producer.OnDeath(this);
        }
        if(squad != null){
            squad.RemoveUnit(this);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.GetComponent<Unit>() != null){
            inRange.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.GetComponent<Unit>() != null){
            inRange.Remove(col.gameObject);
        }
    }    

    public void ToggleSelection(bool toggle) {
        isSelected = toggle;
        GameObject outlineObject = transform.Find("outline").gameObject;
        outlineObject.SetActive(toggle);
    }  

    public void MoveToPosition(Vector2 targetPosition) {
        unitMovement.MoveToPosition(targetPosition);
    }

    public bool IsMoving(){
        return unitMovement.moving;
    }

    public bool AtPosition(Vector2 position){
        return Vector2.Distance(transform.position, position) <= 0.2f;
    }

    public bool CanMove(){
        return unitMovement != null;
    }

    public void StopMoving(){
        unitMovement.StopMoving();
    }
 
    public void FollowTarget(GameObject targetObject) {
        unitMovement.FollowTarget(targetObject);
    }   

    public void FollowTarget(GameObject targetObject, Vector2 offset) {
        unitMovement.FollowTarget(targetObject, offset);
    }     

    public virtual void OnReachedDestination(GameObject target){

    }
    
}
