using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infectable : MonoBehaviour
{
    public float maxInfectionAmount;
    public float infectionAmount;
    public bool isInfected;

    public void Infect(float amount) {
        if(!isInfected){
            infectionAmount += amount;
            if(infectionAmount >= maxInfectionAmount) {
                infectionAmount = maxInfectionAmount;
                isInfected = true;
                OnInfect();
            }
            else if (infectionAmount <= 0){
                OnCure();
            }
            OnInfectUpdate(amount);
        }
    }

    public virtual void OnCure(){

    }

    public virtual void OnInfect(){

    }

    public virtual void OnInfectUpdate(float amount){

    }
}
