using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infectable : MonoBehaviour
{
    public float maxInfectionAmount;
    public float infectionAmount;
    public bool isInfected;

    public void Infect(float amount) {
        infectionAmount = Mathf.Clamp(infectionAmount + amount, 0f, 10f);
        if(!isInfected && infectionAmount >= maxInfectionAmount) {
            infectionAmount = maxInfectionAmount;
            isInfected = true;
            OnInfect();
        }
        else if (isInfected && infectionAmount <= 0){
            isInfected = false;
            OnCure();
        }
        OnInfectUpdate(amount);
    }

    public virtual void OnCure(){

    }

    public virtual void OnInfect(){

    }

    public virtual void OnInfectUpdate(float amount){

    }
}
