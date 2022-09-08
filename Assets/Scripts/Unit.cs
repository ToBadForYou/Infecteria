using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitMovement unitMovement;
    public List<GameObject> inRange;
    public int health;
    public int currentHealth;
    public int damage;
    public int attackSpeed;
    public float attackTimer;
    public float range = 0.9f;
    public bool aggressive;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        attackTimer = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject closeObject in inRange)
        {
            float distance = Vector2.Distance(transform.position, closeObject.transform.position);
            if(distance + 0.2f <= closestDistance) {
                closest = closeObject;
                closestDistance = distance;
            }
        }
        if (closest != null){
            if (closestDistance > range){
                unitMovement.MoveToTarget(closest);
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

    void AttackTarget(GameObject attackTarget){
        Unit unit = attackTarget.GetComponent<Unit>();
        unit.TakeDamage(damage);
    }

    void TakeDamage(int takenDamage){
        currentHealth -= takenDamage;
        if (currentHealth < 0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        inRange.Add(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        inRange.Remove(col.gameObject);
    }    

    public virtual void OnReachedDestination(GameObject target){

    }
    
}
