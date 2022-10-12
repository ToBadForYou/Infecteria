using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitProducer : MonoBehaviour
{
    public Dictionary<UnitType, UnitProductionData> productionData = new Dictionary<UnitType, UnitProductionData>();

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            foreach(KeyValuePair<UnitType, UnitProductionData> entry in productionData){
                entry.Value.Update();
            }
        }
    }

    public void AddProduction(UnitType unitType, UnitProductionData newProductionData){
        productionData.Add(unitType, newProductionData);
    }

    public int GetAmount(UnitType unitType){
        return productionData[unitType].GetAmount();
    }

    public int WithdrawAll(UnitType unitType){
        return productionData[unitType].WithdrawAll();
    }

    public int WithdrawAmount(UnitType unitType, int amount){
        return productionData[unitType].WithdrawAmount(amount);
    }    

    public void OnReturn(Unit returnedUnit){
        productionData[returnedUnit.unitType].OnReturn();
        Destroy(returnedUnit.transform.root.gameObject);
    }

    public void OnDeath(Unit deadUnit){
        productionData[deadUnit.unitType].OnDeath();
    }    
}
