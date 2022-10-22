using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectQuest : Quest
{
    public override void CheckQuestProgress() {
        int amount = gm.infectedCells;

        if(amount > maxAmount)
            amount = maxAmount;

        progressMesh.text = amount + "/" + maxAmount;
    }
}
