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
        nextUnit = unitProductionTime;
        productionTime = unitProductionTime;
    }

    public void Update(){
        if(availableUnits < maxUnits){
            nextUnit -= Time.deltaTime;
            if(nextUnit < 0){
                nextUnit = productionTime;
                availableUnits += 1;
            }
        }
    }

    public void IncreaseMaximum(int amount){
        maxUnits += amount;
    }

    public int GetMaximumAmount(){
        return maxUnits;
    }

    public int GetSpawnedAmount(){
        return spawnedUnits;
    }

    public int GetAvailableAmount(){
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
