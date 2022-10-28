using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactoryManager : MonoBehaviour
{
    Factory currentFactory;

    public TextMeshProUGUI microbacteriaText;
    public TextMeshProUGUI infectionText;
    public TextMeshProUGUI levelTextMesh;
    public TextMeshProUGUI upgradeTooltip;
    public Button upgradeButton;

    public Transform infectionBar;
    public GameObject buildOptions;
    public int selectedSlot;
    public List<Buildable> availableStructures;
    public List<BuildingSlot> buildSlots;

    public SelectCell selectCell;

    public void SetFactory(Factory factory) {
        currentFactory = factory;
        if(factory.currentLevel == factory.maxLevel){
            upgradeButton.interactable = false;
            upgradeTooltip.text = "Fully upgraded";
        }
        else {
            upgradeButton.interactable = true;
            upgradeTooltip.text = factory.upgradeCost + " sugar to upgrade";
        }
        UpdateMicrobacteria(factory.GetBacteriaAmount(), factory.GetMaximumBacteria());
        UpdateInfection(factory.infectionAmount, factory.maxInfectionAmount);
        // TODO Make a script for build slots to handle setting images, colors etc
        for (int i = 0; i < factory.structures.Length; i++){
            bool locked = factory.currentLevel <= i;
            Buildable buildable = null;
            if(factory.structures[i] != null){
                buildable = factory.structures[i].GetComponent<Structure>().buildable;
            }
            buildSlots[i].SetStructure(buildable, locked);
        }
    }

    public void UpdateInfection(float amount, float maxAmount){
        float percentage = amount/maxAmount;
        infectionBar.localScale = new Vector2(percentage, infectionBar.localScale.y);
        infectionText.text = "Infection " + percentage * 100 + "%";
    }

    public void UpdateMicrobacteria(int amount, int maxAmount){
        microbacteriaText.text = "Microbacteria: " + amount + "/" + maxAmount;
    }


    public void UpgradeFactory() {
        if(currentFactory.currentLevel < currentFactory.maxLevel && currentFactory.Upgrade()){
            levelTextMesh.text = "Level " + currentFactory.currentLevel;
            if(currentFactory.currentLevel == currentFactory.maxLevel){
                upgradeButton.interactable = false;
                upgradeTooltip.text = "Fully upgraded";
            }
            else {
                upgradeTooltip.text = currentFactory.upgradeCost + " sugar to upgrade";
            }
        }
    }

    public void ToggleInfectCell(){
        if(selectCell.currentFactory == null){
            selectCell.ViewNearbyCells(currentFactory);
        }
        else {
            selectCell.CloseSelection();
        }
    }

    public void DisplayBuildOptions(int buildSlot){
        if(currentFactory.CanBuildAt(buildSlot)){
            buildOptions.SetActive(!buildOptions.activeSelf);
            if(buildOptions.activeSelf){
                selectedSlot = buildSlot;
                GameObject buildingSlot = buildSlots[buildSlot].gameObject;
                RectTransform buildingSlotTransform = buildingSlot.GetComponent<RectTransform>();
                RectTransform buildOptionsTransform = buildOptions.GetComponent<RectTransform>();
                buildOptionsTransform.anchoredPosition = new Vector2(buildingSlotTransform.anchoredPosition.x - 50, buildingSlotTransform.anchoredPosition.y + 50);
            }
            else {
                selectedSlot = -1;
            }
        }
        else {
            buildOptions.SetActive(false);
        } 
    }

    public void BuildStructure(int structureIndex){
        if(currentFactory.CanBuildAt(selectedSlot) && selectedSlot != -1 && structureIndex != -1 && structureIndex < availableStructures.Count){
            Buildable buildable = availableStructures[structureIndex];
            if(currentFactory.CanBuild(buildable)){
                buildOptions.SetActive(false);
                currentFactory.Build(selectedSlot, availableStructures[structureIndex]);
                buildSlots[selectedSlot].SetStructure(buildable, false);
            }
        }
    }
}
