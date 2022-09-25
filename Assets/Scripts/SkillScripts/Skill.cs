using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isActive;
    public int cost;
    public Skill prevSkill;
    public Image sr;
    public bool hovering;

    public float effect;

    public void OnPointerEnter(PointerEventData eventData) {
        if(!isActive && prevSkill.isActive) {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        }
    }

    public void Upgrade() {
        if(!isActive && prevSkill.isActive) {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            if(gm.DNAPoints >= cost) {
                gm.DNAPoints -= cost;
                isActive = true;
                sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                Skilltree tree = GameObject.Find("skilltree").GetComponent<Skilltree>();
                switch (gameObject.name)
                {
                    case "hp":
                        tree.activeHPSkill = this;
                        break;
                    case "speed":
                        tree.activeSpeedSkill = this;
                        break;
                    case "infection":
                        tree.activeInfectionRateSkill = this;
                        break;
                }
                tree.NotifyPlayer();
            }  
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if(!isActive && prevSkill.isActive) {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
        }
    }
}
