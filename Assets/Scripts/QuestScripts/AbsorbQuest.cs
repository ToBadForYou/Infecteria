using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbQuest : Quest
{
    public override void CheckQuestProgress() {
        int amount = gm.absorbedCells;

        if(amount > maxAmount)
            amount = maxAmount;
        
        progressMesh.text = amount + "/" + maxAmount;
    }
}
