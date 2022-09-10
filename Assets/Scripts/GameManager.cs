using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public int DNAPoints = 0;
    
    private int absorbedCells = 0;
    private int infectedCells = 0;
    private int factoryAmount = 0;
    private int cellAmount;
    public List<GameObject> cells = new List<GameObject>();

    void Start() {
        StartCoroutine(LateStart(0.0f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cellAmount = GameObject.FindGameObjectsWithTag("Cell").Length;
    }

    public void UpdateTextMesh() {
        textMesh.text = "DNA Points: " + DNAPoints + "\nAbsorbed Cells: " + absorbedCells + "\nInfected Cells: " + infectedCells + " / " + cellAmount + "\nFactories: " + factoryAmount;
    }

    public void IncreaseDNAPoints(int amount) {
        DNAPoints += amount;
        UpdateTextMesh();
    }

    public void IncreaseAbsorbedCells() {
        absorbedCells++;
        UpdateTextMesh();
    }

    public void IncreaseInfectedCells() {
        infectedCells++;
        UpdateTextMesh();
    }

    public void IncreaseFactoryAmount() {
        factoryAmount++;
        UpdateTextMesh();
    }

    public void RemoveCell(GameObject cell){
        cells.Remove(cell);
    }

    public void ReplaceCell(GameObject oldCell, GameObject replacement){
        int index = cells.FindIndex(cell => cell == oldCell);
        cells[index] = replacement;
    }

    public void AddCell(GameObject cell){
        cells.Add(cell);
    }
}
