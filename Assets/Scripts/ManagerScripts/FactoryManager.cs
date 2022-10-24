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
    public GameObject buildSlots;
    public int selectedSlot;
    public Sprite emptySprite;
    public Sprite lockedSprite;
    public List<Buildable> availableStructures;

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
            string name = "Empty";
            Sprite sprite = emptySprite;
            Color spriteColor = Color.white;
            bool enableButton = true;
            if(factory.structures[i] != null){
                // TODO read from buildable object instead
                SpriteRenderer spriteRenderer = factory.structures[i].GetComponent<SpriteRenderer>();
                string[] splitArray =  factory.structures[i].name.Split(char.Parse("("));
                name = splitArray[0];
                sprite = spriteRenderer.sprite;
                spriteColor = spriteRenderer.color;
                enableButton = false;
            } else if(factory.currentLevel <= i){
                name = "Locked";
                sprite = lockedSprite;
                enableButton = false;
            }
            SetStructure(i, enableButton, name, sprite, spriteColor);
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

    void SetStructure(int slot, bool enableButton, string text, Sprite sprite, Color spriteColor){
        Transform buildingSlotTransform = buildSlots.transform.Find("buildingSlot" + slot);
        if(text == "Locked"){
            // Temp whacky check until it has its own script
            buildingSlotTransform.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.33f);
        } 
        else {
            buildingSlotTransform.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        buildingSlotTransform.gameObject.GetComponent<Button>().enabled = enableButton;
        Image slotImage = buildingSlotTransform.Find("buildingIcon").GetComponent<Image>();
        buildingSlotTransform.Find("buildingText").GetComponent<TextMeshProUGUI>().text = text;
        slotImage.sprite = sprite;
        slotImage.color = spriteColor;
    }

    public void DisplayBuildOptions(int buildSlot){
        if(currentFactory.CanBuildAt(buildSlot)){
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
                SetStructure(selectedSlot, false, buildable.structureName, buildable.GetSprite(), buildable.GetColor());
            }
        }
    }
}
