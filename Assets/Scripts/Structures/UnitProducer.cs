using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitProducer : MonoBehaviour
{
    public Dictionary<UnitType, UnitProductionData> productionData = new Dictionary<UnitType, UnitProductionData>();

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE)
            foreach(KeyValuePair<UnitType, UnitProductionData> entry in productionData)
                entry.Value.Update();
    }

    public void IncreaseMaximumUnit(UnitType unitType, int amount){
        productionData[unitType].IncreaseMaximum(amount);
    }

    public void AddProduction(UnitType unitType, UnitProductionData newProductionData){
        productionData.Add(unitType, newProductionData);
    }

    public float GetProductionProgress(UnitType unitType){
        if(productionData.ContainsKey(unitType)){
            return productionData[unitType].GetProductionProgress();
        };
        return 1f;
    }

    public int GetAvailableAmount(UnitType unitType){
        if(productionData.ContainsKey(unitType)){
            return productionData[unitType].GetAvailableAmount();
        }
        return 0;
    }

     public int GetMaximumAmount(UnitType unitType){
        if(productionData.ContainsKey(unitType)){
            return productionData[unitType].GetMaximumAmount();
        }
        return 0;
    }   

    public int GetSpawnedAmount(UnitType unitType){
        if(productionData.ContainsKey(unitType)){
            return productionData[unitType].GetSpawnedAmount();
        }
        return 0;
    }

    public int WithdrawAll(UnitType unitType){
        if(productionData.ContainsKey(unitType))
            return productionData[unitType].WithdrawAll();
        return 0;
    }

    public int WithdrawAmount(UnitType unitType, int amount){
        return productionData[unitType].WithdrawAmount(amount);
    }    

    public void OnReturn(Unit returnedUnit){
        if(productionData.ContainsKey(returnedUnit.unitType)){
            productionData[returnedUnit.unitType].OnReturn();
        }
        Destroy(returnedUnit.transform.root.gameObject);
    }

    public void OnDeath(Unit deadUnit){
        if(productionData.ContainsKey(deadUnit.unitType)){
            productionData[deadUnit.unitType].OnDeath();
        }
    }    
}
