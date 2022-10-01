using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public UnitMovement unitMovement;
    GameObject targetObject;
    int damage;

    void Update(){
        if(damage != 0 && targetObject == null){
            Destroy(gameObject);
        }
    }

    public void FireAt(GameObject target, int projectileDamage){
        unitMovement.FollowTarget(target);
        targetObject = target;
        damage = projectileDamage;
    }

    void OnTriggerEnter2D(Collider2D col){
        Unit hit = col.gameObject.GetComponent<Unit>();
        if(col.gameObject == targetObject){
            if(hit != null){
                hit.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    } 
}
