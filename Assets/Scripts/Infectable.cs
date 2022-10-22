using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infectable : MonoBehaviour
{
    public float maxInfectionAmount;
    public float infectionAmount;
    public bool isInfected;
    public Organ organ;

    public void Infect(float amount) {
        if(amount < 0.0f)
            OnGettingCured();

        infectionAmount = Mathf.Clamp(infectionAmount + amount, 0f, maxInfectionAmount);
        if(!isInfected && infectionAmount >= maxInfectionAmount) {
            infectionAmount = maxInfectionAmount;
            isInfected = true;
            OnInfect();
        }
        else if (isInfected && infectionAmount <= 0){
            isInfected = false;
            infectionAmount = 0;
            OnCure();
        }
        OnInfectUpdate(amount);
    }

    public virtual void OnCure(){

    }

    public virtual void OnGettingCured() {

    }

    public virtual void OnInfect(){

    }

    public virtual void OnInfectUpdate(float amount){

    }
}
