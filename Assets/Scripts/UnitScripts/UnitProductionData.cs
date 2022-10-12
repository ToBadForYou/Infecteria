using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProductionData
{
    int availableUnits;
    int maxUnits;
    int productionTime;
    int spawnedUnits = 0;
    float nextUnit = 0;

    public UnitProductionData(int startingUnits, int maximumUnits, int unitProductionTime){
        availableUnits = startingUnits;
        maxUnits = maximumUnits;
        productionTime = unitProductionTime;
    }

    public void Update(){
        nextUnit -= Time.deltaTime;
        if(availableUnits < maxUnits && nextUnit < 0){
            nextUnit = productionTime;
            availableUnits += 1;
        }
    }

    public int GetAmount(){
        return availableUnits - spawnedUnits;
    }

    public int WithdrawAll(){
        int withdrawAmount = availableUnits - spawnedUnits;
        spawnedUnits += withdrawAmount;
        return withdrawAmount;
    }    

    public int WithdrawAmount(int amount){
        int available = availableUnits - spawnedUnits;
        if(amount <= available){
            spawnedUnits += amount;
            return amount;
        }
        return 0;
    } 

    public void OnReturn(){
        spawnedUnits -= 1;
    } 

    public void OnDeath(){
        availableUnits -= 1;
        spawnedUnits -= 1;
    }
}
