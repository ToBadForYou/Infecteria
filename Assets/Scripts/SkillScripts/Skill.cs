using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool isActive;
    public int cost;
    public Skill prevSkill;
    public SpriteRenderer sr;

    public float effect;

    void OnMouseEnter() {
        if(!isActive && prevSkill.isActive) {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        }
    }

    void OnMouseOver() {
        if(!isActive && prevSkill.isActive) {
            if(Input.GetMouseButtonDown(0)) {
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
    }

    void OnMouseExit() {
        if(!isActive && prevSkill.isActive) {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
        }
    }
}
