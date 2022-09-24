using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSquad : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private float offsetFactor = 0.75f;
    public AI AIBehaviour;

    void Update(){
        if(AIBehaviour != null){
            AIBehaviour.Update();
        }
    }

    public List<Unit> GetUnits(){
        return units;
    }

    public void Clear(){
        units.Clear();
    }

    public void AddUnits(List<Unit> unit){
        units.AddRange(unit);
    }

    public void AddUnit(Unit unit){
        if(!units.Contains(unit)){
            units.Add(unit);
        }
    }

    public void ToggleSelection(bool toggle){
        foreach (Unit unit in units){
            unit.ToggleSelection(toggle);
        } 
    }

    public void Infect(Infectable target){
        foreach (Unit unit in units){
            InfectUnit infectUnit = unit.gameObject.GetComponent<InfectUnit>();
            if(infectUnit != null){
                unit.CancelTasks();
                unit.GiveTask(new InfectTask(infectUnit, target.gameObject));
            }
        }        
    }

    public void Search(Vector2 pos, Scout searchUnit){
        List<Infectable> potentialInfections = new List<Infectable>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 15);
        foreach (Collider2D collider in colliders)
        {
            Infectable infectable = collider.gameObject.GetComponent<Infectable>();
            if(infectable != null){
                potentialInfections.Add(infectable);
            }
        }

        searchUnit.GiveTask(new SearchTask(searchUnit, potentialInfections, this)); 
        foreach (Unit unit in units){
            if(unit != (Unit)searchUnit){
                unit.CancelTasks();
                unit.GiveTask(new FollowTask(unit, searchUnit.gameObject, new Vector2(Random.Range(-offsetFactor, offsetFactor), Random.Range(-offsetFactor, offsetFactor))));
            }
        }  
    }

    public void Attack(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }            
            unit.GiveTask(new AttackTask(unit, target));
        }       
    }

    public void Follow(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new FollowTask(unit, target, new Vector2(Random.Range(-offsetFactor, offsetFactor), Random.Range(-offsetFactor, offsetFactor))));
        }
    }

    public void MoveTo(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new MoveTask(unit, target));
        }
    }

    public void MoveTo(Vector2 pos, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new MoveTask(unit, pos));
        }
    }
}
