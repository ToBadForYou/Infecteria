using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skilltree : MonoBehaviour
{
    public Skill activeHPSkill;
    public Skill activeSpeedSkill;
    public Skill activeInfectionRateSkill;

    public void NotifyPlayer() {
        GameObject player = GameObject.Find("Player");
        if(activeSpeedSkill)
            player.GetComponent<PlayerMovement>().additionalSpeed = activeSpeedSkill.effect;
        if(activeInfectionRateSkill)
            player.GetComponent<Player>().additionalInfection = activeInfectionRateSkill.effect;
        if(activeHPSkill)
            player.GetComponent<Player>().stats.IncreaseAdditionalHealth((int)activeHPSkill.effect);
    }
}
