using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSquad : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private float offsetFactor = 0.75f;
    public AI AIBehaviour;

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            if(AIBehaviour != null){
                AIBehaviour.Update();
            }
        }
    }

    public List<Unit> GetUnits(){
        return units;
    }

    public void Clear(){
        units.Clear();
    }

    public void AddUnits(List<Unit> newUnits){
        foreach (Unit unit in newUnits){
            AddUnit(unit);
        }
    }

    public void AddUnit(Unit addUnit){
        if(!units.Contains(addUnit)){
            units.Add(addUnit);
            addUnit.squad = this;
        }
    }

    public void RemoveUnits(List<Unit> removeUnits){
        foreach (Unit unit in removeUnits){
            RemoveUnit(unit);
        }
    }

    public void RemoveUnit(Unit removeUnit){
        if(units.Contains(removeUnit)){
            units.Remove(removeUnit);
            removeUnit.squad = null;
        }
    }

    public void ToggleSelection(bool toggle){
        foreach (Unit unit in units){
            unit.ToggleSelection(toggle);
        } 
    }

    public void SetStance(bool aggressive){
        foreach (Unit unit in units){
            unit.SetStance(aggressive);
        }     
    }

    public void HostileDetected(GameObject hostile){
        foreach (Unit unit in units){
            if(unit.stats.IsAggressive() && unit.GetTaskType() != TaskType.ATTACK){
                unit.GiveTask(new AttackTask(unit, hostile), true);
            }
        }
    }

    public void Infect(Infectable target, bool cancelTasks){
        foreach (Unit unit in units){
            InfectUnit infectUnit = unit.gameObject.GetComponent<InfectUnit>();
            if(infectUnit != null){
                if(cancelTasks){
                    unit.CancelTasks();
                }    
                unit.GiveTask(new InfectTask(infectUnit, target.gameObject), false);
            }
        }        
    }

    public void Search(Vector2 pos, Scout searchUnit){
        List<Infectable> potentialInfections = new List<Infectable>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 15);
        foreach (Collider2D collider in colliders){
            Infectable infectable = collider.gameObject.GetComponent<Infectable>();
            if(infectable != null){
                potentialInfections.Add(infectable);
            }
        }
        Search(searchUnit, potentialInfections);
    }

    public void Search(Scout searchUnit, List<Infectable> potentialInfections){
        searchUnit.GiveTask(new SearchTask(searchUnit, potentialInfections, this), false); 
        foreach (Unit unit in units){
            if(unit != (Unit)searchUnit){
                unit.CancelTasks();
                unit.GiveTask(new FollowTask(unit, searchUnit.gameObject, new Vector2(Random.Range(-offsetFactor, offsetFactor), Random.Range(-offsetFactor, offsetFactor))), false);
            }
        }          
    }

    public void Patrol(Vector2 start, Vector2 end, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }            
            unit.GiveTask(new PatrolTask(unit, start, end), false);
        }       
    }

    public void Attack(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }            
            unit.GiveTask(new AttackTask(unit, target), false);
        }       
    }

    public void Follow(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new FollowTask(unit, target, new Vector2(Random.Range(-offsetFactor, offsetFactor), Random.Range(-offsetFactor, offsetFactor))), false);
        }
    }

    public void Return(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new ReturnTask(unit, target), false);
        }
    }

    public void MoveTo(GameObject target, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new MoveTask(unit, target), false);
        }
    }

    public void MoveTo(Vector2 pos, bool cancelTasks){
        foreach (Unit unit in units){
            if(cancelTasks){
                unit.CancelTasks();
            }
            unit.GiveTask(new MoveTask(unit, pos), false);
        }
    }
}
