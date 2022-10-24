using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI DNAPointsText;
    public TextMeshProUGUI sugarText;
    public TextMeshProUGUI textMesh;
    public GameObject cantAfford;

    [SerializeField] private int DNAPoints = 0;
    [SerializeField] private int sugar = 0;
    
    public int absorbedCells = 0;
    public int infectedCells = 0;
    public int factoryAmount = 0;
    public int infectedHeartCells = 0;

    // Additional stats (only used for tutorial right now)
    public int timesEnteredCell = 0;
    public int producedMicrobacterias = 0;
    public int timesDetectedByScout = 0;
    public int timesDetectedByDetector = 0;
    public int timesOpenedPlayerCommands = 0;

    private int cellAmount;
    public bool won;
    float affordTimer = 0;
    
    public List<GameObject> cells = new List<GameObject>();

    TutorialHandler th;

    int lastSugar;
    int lastDNA;

    public bool skillTreeOpen;
    public bool mapOpen;
    public bool instructionOpen;
    public bool tutorialPopup;

    void Start(){
        lastSugar = sugar;
        lastDNA = DNAPoints;
        StartCoroutine(LateStart(0.0f));
    }

    void Update() {
        if(th){
            if(absorbedCells == 1 && th.events.ContainsKey(EventType.Absorbed))
                th.TriggerEvent(EventType.Absorbed);
            if(infectedCells == 1 && th.events.ContainsKey(EventType.Infected))
                th.TriggerEvent(EventType.Infected);
            if(factoryAmount == 1 && th.events.ContainsKey(EventType.MadeFactory))
                th.TriggerEvent(EventType.MadeFactory);
            if(timesEnteredCell == 1 && th.events.ContainsKey(EventType.EnteredCell))
                th.TriggerEvent(EventType.EnteredCell);
            if(producedMicrobacterias == 1 && th.events.ContainsKey(EventType.Microbacteria))
                th.TriggerEvent(EventType.Microbacteria);
            if(timesDetectedByScout == 1 && th.events.ContainsKey(EventType.ScoutDetected))
                th.TriggerEvent(EventType.ScoutDetected);
            if(timesDetectedByDetector == 1 && th.events.ContainsKey(EventType.DetectorDetected))
                th.TriggerEvent(EventType.DetectorDetected);
            if(timesOpenedPlayerCommands == 1 && th.events.ContainsKey(EventType.UnitSelected))
                th.TriggerEvent(EventType.UnitSelected);
            if(sugar > lastSugar && th.events.ContainsKey(EventType.CollectedSugar))
                th.TriggerEvent(EventType.CollectedSugar);
            if(DNAPoints > lastDNA && th.events.ContainsKey(EventType.CollectedDNA))
                th.TriggerEvent(EventType.CollectedDNA);
        } 
        if(affordTimer != 0){
            affordTimer -= Time.deltaTime;
            if(affordTimer < 0){
                affordTimer = 0;
                cantAfford.SetActive(false);
            }
        } 
    }

    public bool CanAffordSugar(int amount){
        bool haveEnough = sugar >= amount;
        if(!haveEnough){
            CantAfford();
        }
        return haveEnough;
    }

    public bool CanAffordDNA(int amount){
        bool haveEnough = DNAPoints >= amount;
        if(!haveEnough){
            CantAfford();
        }
        return haveEnough;
    }

    public void CantAfford(){
        cantAfford.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 16);
        affordTimer = 0.8f;
        cantAfford.SetActive(true);
        /*
        Image image = cantAfford.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        TextMeshProUGUI text = cantAfford.GetComponent<TextMeshProUGUI>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        cantAfford.AddComponent<FadeOutEffect>();*/
    }   

    public bool ShouldUnPause(){
        int count = 0;
        if(mapOpen){
            count += 1;
        }
        if(skillTreeOpen){
            count += 1;
        }
        if(instructionOpen){
            count += 1;
        }
        if(tutorialPopup){
            count += 1;
        }

        return count < 1;
    }

    IEnumerator LateStart(float waitTime){
        yield return new WaitForSeconds(waitTime);
        cellAmount = GameObject.FindGameObjectsWithTag("Cell").Length;
        th = GameObject.Find("Tutorial Handler").GetComponent<TutorialHandler>();
    }

    public void SetWinStats(){
        string infectedStats = "infected cells:          " + infectedCells + "\n";
        string absorbedStats = "absorbed cells:          " + absorbedCells + "\n";
        string factoriesStats = "factories:               " + factoryAmount + "\n";
        string bacteriaProduced = "produced microbacteria   " + producedMicrobacterias + "\n";
        string scoutStats = "detection by scout:      " + timesDetectedByScout + "\n";
        string detectorStats = "detection by detector:   " + timesDetectedByDetector + "\n";

        GameObject.Find("stats").GetComponent<TextMeshProUGUI>().text = infectedStats + absorbedStats + factoriesStats + bacteriaProduced + scoutStats + detectorStats;
        if(!won){
            GameObject.Find("title").GetComponent<TextMeshProUGUI>().text = "Dead: you loose!";
        }
    }

    public void UpdateTextMesh() {
        textMesh.text = "Absorbed Cells: " + absorbedCells + "\nInfected Cells: " + infectedCells + " / " + cellAmount + "\nFactories: " + factoryAmount;
    }

    public void IncreaseSugar(int amount) {
        lastSugar = sugar;
        sugar += amount;
        sugarText.text = sugar.ToString();
    }

    public int GetSugarAmount() {
        return sugar;
    }

    public void DecreaseDNAPoints(int amount) {
        lastDNA = DNAPoints;
        DNAPoints -= amount;
        DNAPointsText.text = DNAPoints.ToString();
    }

    public void IncreaseDNAPoints(int amount) {
        lastDNA = DNAPoints;
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