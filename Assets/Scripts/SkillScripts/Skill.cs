using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] private Skilltree skilltree;
    [SerializeField] private Button nextButton;

    public int cost;
    public float effect;
    
    bool isClicked;

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
        if(!isClicked) {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            if(gm.GetDNAPointAmount() >= cost) {
                gm.DecreaseDNAPoints(cost);
                switch (gameObject.name)
                {
                    case "hp":
                        skilltree.activeHPSkill = this;
                        SetButtonBought();
                        ActivateNextButton();
                        break;
                     case "speed":
                        skilltree.activeSpeedSkill = this;
                        SetButtonBought();
                        ActivateNextButton();
                        break;
                    case "infection":
                        skilltree.activeInfectionRateSkill = this;
                        SetButtonBought();
                        ActivateNextButton();
                        break;
                }
                skilltree.NotifyPlayer();
                isClicked = true;
            }
        }
    }
}
