using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProductionData
{
    int currentUnits;
    int maxUnits;
    int productionTime;
    float nextUnit = 0;

    public UnitProductionData(int startingUnits, int maximumUnits, int unitProductionTime){
        currentUnits = startingUnits;
        maxUnits = maximumUnits;
        productionTime = unitProductionTime;
    }

    public void Update(){
        nextUnit -= Time.deltaTime;
        if(currentUnits < maxUnits && nextUnit < 0){
            nextUnit = productionTime;
            currentUnits += 1;
        }
    }

    public int GetAmount(){
        return currentUnits;
    }

    public int WithdrawAll(){
        int temp = currentUnits;
        currentUnits = 0;
        return temp;
    }    

    public int WithdrawAmount(int amount){
        if(amount <= currentUnits){
            currentUnits -= amount;
            return amount;
        }
        return 0;
    } 

    public void OnReturn(){
        currentUnits += 1;
    }  
}
