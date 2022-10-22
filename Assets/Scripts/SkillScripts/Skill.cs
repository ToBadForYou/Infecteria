using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Skill : MonoBehaviour
{
    [SerializeField] private Skilltree skilltree;
    [SerializeField] private Button nextButton;
    public TextMeshProUGUI tooltip;

    public string desc;
    public int cost;
    public float effect;
    
    bool isBought;

    void Start(){
        tooltip.text = desc + " - Cost: " + cost + " DNA";
    }

    public void SetButtonBought() {
        Button button = GetComponent<Button>();

        ColorBlock cb = button.colors;
        cb.normalColor = new Color(1.0f, 1.0f, 0.5f);
        cb.highlightedColor = new Color(1.0f, 1.0f, 0.5f);
        cb.pressedColor = new Color(1.0f, 1.0f, 0.5f);
        cb.selectedColor = new Color(1.0f, 1.0f, 0.5f);
        button.colors = cb;
    }

    public void ActivateNextButton() {
        if(nextButton) {
            nextButton.interactable = true;
        }
    }

    public void MakeActive() {
        if(!isBought) {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            if(gm.GetDNAPointAmount() >= cost) {
                gm.DecreaseDNAPoints(cost);
                switch (gameObject.name){
                    case "hp":
                        skilltree.activeHPSkill = this;
                        break;
                     case "speed":
                        skilltree.activeSpeedSkill = this;
                        break;
                    case "infection":
                        skilltree.activeInfectionRateSkill = this;
                        break;
                }
                SetButtonBought();
                ActivateNextButton();
                skilltree.NotifyPlayer();
                tooltip.text = desc + " - Upgraded";
                isBought = true;
            }
        }
    }
}
