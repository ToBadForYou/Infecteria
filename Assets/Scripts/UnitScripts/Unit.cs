using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{   
    public enum Faction { bacteria, immuneSystem, neutral };
    public Transform healthBar;
    public UnitMovement unitMovement;
    public List<GameObject> inRange;

    //TODO: use 'public UnitStats stats;' instead
    public int health;
    public int currentHealth;
    public int damage;
    public int attackSpeed;
    public float attackTimer;
    public float range = 0.9f;
    public bool aggressive;

    public float additionalHp = 0.0f;

    public Faction owner;
    public List<Task> currentTasks = new List<Task>();
    public Task proximityHostile;
    public GameObject hitEffect;

    void Start()
    {
        //TODO: stats = new UnitStats();
        currentHealth = health;
    }

    protected void Update()
    {
        if (aggressive){
            if(!CanAttack()){
                attackTimer -= Time.deltaTime;
            }
            FindHostileInProximity();
        }
        UpdateCurrentTask();
    }

    public void CancelTasks(){
        currentTasks.Clear();
    }

    public void FindHostileInProximity(){
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject closeObject in inRange)
        {
            if(IsHostile(closeObject.GetComponent<Unit>())){
                float distance = Vector2.Distance(transform.position, closeObject.transform.position);
                if(distance + 0.2f <= closestDistance) {
                    closest = closeObject;
                    closestDistance = distance;
                }
            }
        }

        if (closest != null && (proximityHostile == null || closest != proximityHostile.GetTarget())){
            proximityHostile = new AttackTask(this, closest);
        }       
    }

    public void UpdateCurrentTask(){
        Task currentTask = null;
        if(proximityHostile != null){
            currentTask = proximityHostile;
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

    public void GiveTask(Task task){
        currentTasks.Add(task);
    }

    public bool InRange(Vector2 position){
        return Vector2.Distance(transform.position, position) <= range;
    }

    public bool IsHostile(Unit unit){
        return unit.owner != Faction.neutral && owner != unit.owner;
    }

    public bool CanAttack(){
        return attackTimer <= 0;
    }

    public bool AttackTarget(GameObject attackTarget){
        if(CanAttack()){
            attackTimer = attackSpeed;
            Unit unit = attackTarget.GetComponent<Unit>();
            return unit.TakeDamage(damage);
        }
        return false;
    }

    bool TakeDamage(int takenDamage){
        currentHealth -= takenDamage;
        OnTakeDamage();
        healthBar.localScale = new Vector2((float)currentHealth/health, healthBar.localScale.y);
        if (currentHealth <= 0){
            OnDeath();
            Destroy(gameObject.transform.root.gameObject);
            return true;
        } 
        return false;
    }

    public virtual void OnTakeDamage() {
        Instantiate(hitEffect, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
    }

    public virtual void OnDeath() {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Unit>() != null){
            inRange.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Unit>() != null){
            inRange.Remove(col.gameObject);
        }
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
