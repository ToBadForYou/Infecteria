using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSlot : MonoBehaviour
{
    public Button button;
    public Image slotIcon;
    public Image buildingIcon;
    public TextMeshProUGUI buildingText;

    public Sprite emptySprite;
    public Sprite lockedSprite;    

    public void SetStructure(Buildable buildable, bool locked){
        bool hasBuilding = buildable != null;
        if(locked){
            slotIcon.color = new Color(1, 1, 1, 0.33f);
            buildingText.text = "Locked";
            button.enabled = false;
            buildingIcon.sprite = lockedSprite;
        }
        else {
            slotIcon.color = Color.white;
            if(hasBuilding){
                button.enabled = false;
                buildingText.text = buildable.structureName;
                buildingIcon.sprite = buildable.GetSprite();
                buildingIcon.color = buildable.GetColor();
            }
            else {
                buildingText.text = "Empty";
                button.enabled = true;
                buildingIcon.sprite = emptySprite;
            }
        }
        if(!hasBuilding){
            buildingIcon.color = Color.white;
        }
    }

}
