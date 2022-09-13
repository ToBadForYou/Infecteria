using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{   
    public enum Faction { bacteria, immuneSystem, neutral };
    public Transform healthBar;
    public UnitMovement unitMovement;
    public List<GameObject> inRange;
    public int health;
    public int currentHealth;
    public int damage;
    public int attackSpeed;
    public float attackTimer;
    public float range = 0.9f;
    public bool aggressive;
    public Faction owner;

    void Start()
    {
        currentHealth = health;
        attackTimer = attackSpeed;
    }

    protected void Update()
    {
        if (aggressive){
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

            if (closest != null){
                if (closestDistance > range){
                    unitMovement.FollowTarget(closest);
                } else {
                    unitMovement.StopMoving();
                    attackTimer -= Time.deltaTime;
                    if (attackTimer < 0){
                        AttackTarget(closest);
                        attackTimer = attackSpeed;
                    }
                }
            }
        }
    }

    public bool IsHostile(Unit unit){
        return unit.owner != Faction.neutral && owner != unit.owner;
    }

    void AttackTarget(GameObject attackTarget){
        Unit unit = attackTarget.GetComponent<Unit>();
        unit.TakeDamage(damage);
    }

    void TakeDamage(int takenDamage){
        currentHealth -= takenDamage;
        OnTakeDamage();
        if (currentHealth < 0){
            OnDeath();
            Destroy(gameObject.transform.root.gameObject);
        }
        healthBar.localScale = new Vector2((float)currentHealth/health, healthBar.localScale.y);
    }

    public virtual void OnTakeDamage() {
        
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

    public virtual void OnReachedDestination(GameObject target){

    }
    
}
