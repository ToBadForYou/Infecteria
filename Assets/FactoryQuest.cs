using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryQuest : Quest
{
    public override void CheckQuestProgress() {
        int amount = gm.factoryAmount;

        if(amount > maxAmount)
            amount = maxAmount;

        progressMesh.text = amount + "/" + maxAmount;
    }
}
