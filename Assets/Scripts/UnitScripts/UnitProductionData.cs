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

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            nextUnit -= Time.deltaTime;
            if(currentUnits < maxUnits && nextUnit < 0){
                nextUnit = productionTime;
                currentUnits += 1;
            }
        }
    }
}
