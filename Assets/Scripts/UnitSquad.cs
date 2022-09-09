using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSquad : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddUnits(List<Unit> unit){
        units.AddRange(unit);
    }

    public void AddUnit(Unit unit){
        units.Add(unit);
    }

    public void MoveTo(Vector2 pos){
        foreach (Unit unit in units)
        {
            unit.unitMovement.MoveToPosition(pos);
        }
    }
}
