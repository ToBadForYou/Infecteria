using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartQuest : Quest
{
    public override void InitQuest(){
        maxAmount = GameObject.Find("Heart").GetComponent<Organ>().countOrganCells;
        CheckQuestProgress();
    }

    public override void CheckQuestProgress() {
        int amount = gm.infectedHeartCells;

        if(amount > maxAmount)
            amount = maxAmount;

        progressMesh.text = amount + "/" + maxAmount;
    }
}
