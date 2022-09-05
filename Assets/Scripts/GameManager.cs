using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int DNAPoints = 0;
    
    private int absorbedCells = 0;
    private int infectedCells = 0;
    private int factoryAmount = 0;

    public void IncreaseDNAPoints(int amount) {
        DNAPoints += amount;
    }

    public void IncreaseAbsorbedCells() {
        absorbedCells++;
    }

    public void IncreaseInfectedCells() {
        infectedCells++;
    }

    public void IncreaseFactoryAmount() {
        factoryAmount++;
    }
}
