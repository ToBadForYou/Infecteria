using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactoryManager : MonoBehaviour
{
    Factory currentFactory;
    public TextMeshProUGUI microbacteriaText;
    public TextMeshProUGUI infectionText;
    public Transform infectionBar;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetFactory(Factory factory){
        currentFactory = factory;
        UpdateMicrobacteria(2, 5);
        UpdateInfection(factory.infectionAmount, factory.maxInfectionAmount);
    }

    public void UpdateInfection(float amount, float maxAmount){
        float percentage = amount/maxAmount;
        infectionBar.localScale = new Vector2(percentage, infectionBar.localScale.y);
        infectionText.text = "Infection " + percentage*100 + "%";
    }

    public void UpdateMicrobacteria(int amount, int maxAmount){
        microbacteriaText.text = "Microbacteria: " + amount + "/" + maxAmount;
    }

    public void BuildStructure(int buildSlot){
        currentFactory.Build();
    }
}
