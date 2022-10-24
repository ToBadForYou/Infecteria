using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrobacteriaProducer : Structure
{
    public Transform barPivot;
    public UnitProducer unitProducer;

    void Start(){
        unitProducer.AddProduction(UnitType.MICROBACTERIA, new UnitProductionData(0, 1, 15));
    }

    void Update(){
        // TODO Actually spawn and add microbacterias to the factory
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            barPivot.localScale = new Vector2(1.0f - unitProducer.GetProductionProgress(UnitType.MICROBACTERIA), 1.0f);
            int withdrawAmount = unitProducer.WithdrawAmount(UnitType.MICROBACTERIA, 1);
            if(withdrawAmount > 0){
            }
        }
    }
}
