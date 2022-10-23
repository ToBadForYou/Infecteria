using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{
    public GameObject projectilePrefab;
    new void Start() {
        base.Start();
        SetUnitStats(50, 50, 2, 1, 1.0f, 5f, true);
    }

    public override bool AttackTarget(GameObject attackTarget){
        if(CanAttack()){
            stats.SetAttackTimer(stats.GetAttackSpeed());
            Vector3 dir = (attackTarget.transform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position + dir*0.8f, Quaternion.identity);
            projectile.GetComponent<Projectile>().FireAt(attackTarget, stats.GetDamage());
        }
        return false;
    }
}
