using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSquad : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private float offsetFactor = 0.75f;

    public void AddUnits(List<Unit> unit){
        units.AddRange(unit);
    }

    public void AddUnit(Unit unit){
        if(!units.Contains(unit)){
            units.Add(unit);
        }
    }

    public void Search(Vector2 pos){
        List<Infectable> potentialInfections = new List<Infectable>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 15);
        foreach (Collider2D collider in colliders)
        {
            Infectable infectable = collider.gameObject.GetComponent<Infectable>();
            if(infectable != null){
                potentialInfections.Add(infectable);
            }
        }

        foreach (InfectUnit unit in units){
            unit.GiveTask(new SearchTask(unit, potentialInfections));
        }  
    }

    public void Attack(GameObject target){
        foreach (Unit unit in units){
            unit.GiveTask(new AttackTask(unit, target));
        }       
    }

    public void Follow(GameObject target){
        foreach (Unit unit in units){
            unit.CancelTasks();
            unit.GiveTask(new FollowTask(unit, target, new Vector2(Random.Range(-offsetFactor, offsetFactor), Random.Range(-offsetFactor, offsetFactor))));
        }
    }

    public void MoveTo(GameObject target){
        foreach (Unit unit in units){
            unit.GiveTask(new MoveTask(unit, target));
        }
    }

    public void MoveTo(Vector2 pos){
        foreach (Unit unit in units){
            unit.GiveTask(new MoveTask(unit, pos));
        }
    }
}
