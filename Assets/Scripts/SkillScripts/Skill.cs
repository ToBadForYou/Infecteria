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
                        ActivateNextButton();
                        break;
                     case "speed":
                        skilltree.activeSpeedSkill = this;
                        ActivateNextButton();
                        break;
                    case "infection":
                        skilltree.activeInfectionRateSkill = this;
                        ActivateNextButton();
                        break;
                }
                skilltree.NotifyPlayer();
                isClicked = true;
            }
        }
    }
}
