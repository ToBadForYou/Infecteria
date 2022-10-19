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
    public GameObject mouseObject;

    public GameObject skilltree;
    public GameObject factoryOverview;
    public GameObject buildOptions;

    public GameObject zoomMap;

    public UnitSquad units;

    public float additionalInfection = 0.0f;

    public AudioSource audioSrc;
    public AudioClip absorbSoundEffect;

    new void Start() {
        base.Start();
        SetUnitStats(100, 100, 1, 1, 1.0f, 0.9f, false);
    }

    public void MakeObjActive(GameObject obj) {
        if(obj) {
            if(!obj.activeSelf) {
                obj.SetActive(true);
            }
        }
    }

    public void MakeObjDeactive(GameObject obj) {
        if(obj) {
            if(obj.activeSelf) {
                obj.SetActive(false);
            }
        }
    }

    public override void OnDeath() {
        SceneManager.LoadScene(0);
    }

    new void Update(){
        hpTextMesh.text = stats.GetCurrentHealth() + "/" + stats.GetHealth();
            // Skilltree
            if(Input.GetKeyDown(KeyCode.T)) {
                if(!skilltree.activeSelf) {
                    skilltree.SetActive(true);
                    PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);
                }
                else {
                    skilltree.SetActive(false);
                    PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
                }
            }

            // Zoom Map
            if(Input.GetKeyDown(KeyCode.M)) {
                if(!zoomMap.activeSelf) {
                    zoomMap.SetActive(true);
                    PauseManager.Instance.SetPauseState(PauseManager.PauseState.FULL);
                }
                else {
                    zoomMap.SetActive(false);
                    PauseManager.Instance.SetPauseState(PauseManager.PauseState.NONE);
                }
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

            // Infecting Cells
            if(currentCell) {
                if(currentCell.isInfected) {
                    MakeObjDeactive(spaceObject);
                    MakeObjActive(eObject);
                    MakeObjActive(qObject);
                    if(Input.GetKeyDown(KeyCode.E)) {
                        audioSrc.clip = absorbSoundEffect;
                        audioSrc.Play();
                        currentCell.GetAbsorbed();

                        GetComponent<PlayerMovement>().SetSpeed(5.0f);
                    }
                    else if(Input.GetKeyDown(KeyCode.Q)) {
                        if(mouseObject) {
                            if(!mouseObject.activeSelf) {
                                MakeObjActive(mouseObject);
                            }
                        }
                        currentCell.TurnIntoFactory();
                    }
                }
                else {
                    MakeObjActive(spaceObject);
                    if(Input.GetKey(KeyCode.Space)) {
                        currentCell.Infect(Time.deltaTime + additionalInfection);
                    }
                }
            }
            else {
                MakeObjDeactive(spaceObject);
                MakeObjDeactive(eObject);
                MakeObjDeactive(qObject);
            }
    }
}
