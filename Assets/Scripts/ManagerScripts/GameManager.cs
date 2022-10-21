using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI DNAPointsText;
    public TextMeshProUGUI sugarText;
    public TextMeshProUGUI textMesh;

    [SerializeField] private int DNAPoints = 0;
    [SerializeField] private int sugar = 0;
    
    public int absorbedCells = 0;
    public int infectedCells = 0;
    public int factoryAmount = 0;
    private int cellAmount;
    public bool won;
    
    public List<GameObject> cells = new List<GameObject>();

    void Start(){
        StartCoroutine(LateStart(0.0f));
    }

    IEnumerator LateStart(float waitTime){
        yield return new WaitForSeconds(waitTime);
        cellAmount = GameObject.FindGameObjectsWithTag("Cell").Length;
    }

    public void SetWinStats(){
        string infectedStats = "infected cells:       " + infectedCells + "\n";
        string absorbedStats = "absorbed cells:       " + absorbedCells + "\n";
        string factoriesStats = "factories:            " + factoryAmount + "\n";
        GameObject.Find("stats").GetComponent<TextMeshProUGUI>().text = infectedStats + absorbedStats + factoriesStats;
        if(!won){
            GameObject.Find("title").GetComponent<TextMeshProUGUI>().text = "Dead: you loose!";
        }
    }

    public void UpdateTextMesh() {
        textMesh.text = "Absorbed Cells: " + absorbedCells + "\nInfected Cells: " + infectedCells + " / " + cellAmount + "\nFactories: " + factoryAmount;
    }

    public void IncreaseSugar(int amount) {
        sugar += amount;
        sugarText.text = sugar.ToString();
    }
    public int GetSugarAmount() {
        return sugar;
    }

    public void DecreaseDNAPoints(int amount) {
        DNAPoints -= amount;
        DNAPointsText.text = DNAPoints.ToString();
    }

    public void IncreaseDNAPoints(int amount) {
        DNAPoints += amount;
        DNAPointsText.text = DNAPoints.ToString();
    }

    public int GetDNAPointAmount() {
        return DNAPoints;
    }

    public void IncreaseAbsorbedCells() {
        absorbedCells++;
        UpdateTextMesh();
    }

    public void IncreaseInfectedCells(int amount) {
        infectedCells += amount;
        UpdateTextMesh();
    }

    public void IncreaseFactoryAmount(int amount) {
        factoryAmount += amount;
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
