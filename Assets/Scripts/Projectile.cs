using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public UnitMovement unitMovement;
    GameObject targetObject;
    int damage;

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(damage != 0 && targetObject == null)
                Destroy(gameObject);
        }
    }

    public void FireAt(GameObject target, int projectileDamage){
        unitMovement.FollowTarget(target);
        targetObject = target;
        damage = projectileDamage;
    }

    void OnCollisionEnter2D(Collision2D col){
        Unit hit = targetObject.gameObject.GetComponent<Unit>();
        if(targetObject.name.Contains("Range"))
            targetObject.name = col.gameObject.name;
        if(col.gameObject.name == targetObject.name){
            if(hit != null){
                hit.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    } 
}
