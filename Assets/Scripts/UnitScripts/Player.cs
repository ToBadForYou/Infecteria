using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Unit
{
    public TextMeshProUGUI hpTextMesh;

    public Cell currentCell;
    public Factory currentFactory;

    public GameObject spaceObject;
    public GameObject eObject;
    public GameObject qObject;
    public GameObject fObject;

    public GameObject skilltree;
    public GameObject factoryOverview;
    public GameObject buildOptions;

    public GameObject zoomMap;
    public GameObject instructionMenu;

    public UnitSquad units;

    public float additionalInfection = 0.0f;

    public AudioSource audioSrc;
    public AudioClip absorbSoundEffect;
    public AudioClip factorySoundEffect;

    new void Start(){
        base.Start();
        SetUnitStats(100, 100, 1, 1, 1.0f, 0.9f, false);
    }

    public void MakeObjActive(GameObject obj){
        if(obj) {
            if(!obj.activeSelf) {
                obj.SetActive(true);
            }
        }
    }

    public void MakeObjDeactive(GameObject obj){
        if(obj) {
            if(obj.activeSelf) {
                obj.SetActive(false);
            }
        }
    }

    public override void OnDeath() {
        DontDestroyOnLoad(GameObject.Find("GameManager"));
        SceneManager.LoadScene("Ending");
    }

    public void ToggleMap(bool show){
        zoomMap.SetActive(show);
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.mapOpen = show;
        if(show){
            PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);
        }
        else{
            if(gameManager.ShouldUnPause()){
                PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
            }
        }
    }

    public void ToggleSkillTree(bool show){
        skilltree.SetActive(show);
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.skillTreeOpen = show;
        if(show){
            PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);    
        }
        else {
            if(gameManager.ShouldUnPause()){
                PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
            }
            skilltree.GetComponent<TooltipManager>().HideTooltips();
        }
    }

    public void ToggleInstructionMenu(bool show){
        instructionMenu.SetActive(show);
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.instructionOpen = show;
        if(show){
            PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);    
        }
        else {
            if(gameManager.ShouldUnPause()){
                PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
            }
        }
    }    

    new void Update(){
        hpTextMesh.text = stats.GetCurrentHealth() + "/" + stats.GetHealth();
        // Skilltree
        if(Input.GetKeyDown(KeyCode.T)){
            ToggleSkillTree(!skilltree.activeSelf);
        }

        // Zoom Map
        if(Input.GetKeyDown(KeyCode.M)){
            ToggleMap(!zoomMap.activeSelf);
        }
        
        // Instruction menu
        if(Input.GetButtonUp("Cancel")){
            ToggleInstructionMenu(!instructionMenu.activeSelf);
        }

        // Handling factories
        if(currentFactory) {
            MakeObjActive(factoryOverview);
            factoryOverview.GetComponent<FactoryManager>().SetFactory(currentFactory);
            if(Input.GetKeyDown(KeyCode.F)) {
                currentFactory.JoinPlayerSquad(gameObject, units);
            }
        } else {
            MakeObjDeactive(factoryOverview);
            MakeObjDeactive(buildOptions);
        }

        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE){
            // Infecting Cells
            if(currentCell) {
                if(currentCell.isInfected) {
                    MakeObjActive(eObject);
                    MakeObjActive(qObject);
                    if(Input.GetKeyDown(KeyCode.E)) {
                        audioSrc.clip = absorbSoundEffect;
                        audioSrc.Play();
                        currentCell.GetAbsorbed();
                        Heal(25);

                        GetComponent<PlayerMovement>().SetSpeedPenalty(0f);
                    }
                    else if(Input.GetKeyDown(KeyCode.Q)) {
                        audioSrc.clip = factorySoundEffect;
                        audioSrc.Play();
                        currentCell.TurnIntoFactory();
                    }
                }
                else {
                    currentCell.Infect(Time.deltaTime + additionalInfection);
                }
            }
            else {
                MakeObjDeactive(spaceObject);
                MakeObjDeactive(eObject);
                MakeObjDeactive(qObject);
            }
        }
    }
}
