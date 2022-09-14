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
    public GameObject buildOptions;
    public GameObject buildSlots;
    public int selectedSlot;
    public List<Buildable> availableStructures;

    public void SetFactory(Factory factory) {
        currentFactory = factory;
        UpdateMicrobacteria(factory.bacteriaAmount, factory.maxBacteriaAmount);
        UpdateInfection(factory.infectionAmount, factory.maxInfectionAmount);
    }

    public void UpdateInfection(float amount, float maxAmount){
        float percentage = amount/maxAmount;
        infectionBar.localScale = new Vector2(percentage, infectionBar.localScale.y);
        infectionText.text = "Infection " + percentage * 100 + "%";
    }

    public void UpdateMicrobacteria(int amount, int maxAmount){
        microbacteriaText.text = "Microbacteria: " + amount + "/" + maxAmount;
    }

    public void DisplayBuildOptions(int buildSlot){
        buildOptions.SetActive(!buildOptions.activeSelf);
        if(buildOptions.activeSelf){
            selectedSlot = buildSlot;
            GameObject buildingSlot = buildSlots.transform.Find("buildingSlot" + buildSlot).gameObject;
            RectTransform buildingSlotTransform = buildingSlot.GetComponent<RectTransform>();
            RectTransform buildOptionsTransform = buildOptions.GetComponent<RectTransform>();
            buildOptionsTransform.anchoredPosition = new Vector2(buildingSlotTransform.anchoredPosition.x - 50, buildingSlotTransform.anchoredPosition.y + 50);
        }
        else {
            selectedSlot = -1;
        }
    }

    public void BuildStructure(int structureIndex){
        if(selectedSlot != -1 && structureIndex != -1 && structureIndex < availableStructures.Count){
            currentFactory.Build(selectedSlot, availableStructures[structureIndex]);
        }
    }
}
