using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProducer : MonoBehaviour
{
    public Unit unit; // TODO restrict to specific unit
    int currentUnits;
    public int maxUnits = 5;
    public int productionTime = 15;
    float nextUnit = 0;

    void Start(){
        currentUnits = maxUnits;
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            nextUnit -= Time.deltaTime;
            if(currentUnits < maxUnits && nextUnit < 0){
                nextUnit = productionTime;
                currentUnits += 1;
            }
        }
    }

    public int WithdrawAll(){
        int current = currentUnits;
        currentUnits = 0;
        return current;
    }

    public int WithdrawUnits(int amount){
        if(amount <= currentUnits){
            return amount;
        }
        return 0;
    }

    public void OnUnitReturn(Unit returnedUnit){
        currentUnits += 1;
        Destroy(returnedUnit.transform.root.gameObject);
    }
}
