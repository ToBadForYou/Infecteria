using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    public Cell currentCell;
    public Factory currentFactory;

    public GameObject spaceObject;
    public GameObject eObject;
    public GameObject qObject;
    public GameObject bObject;
    public GameObject iObject;
    public GameObject fObject;

    public GameObject skilltree;
    public GameObject factoryOverview;
    public GameObject buildOptions;

    public List<Vector2> startPositions;

    void Start()
    {
        //temporarily commented out for debug purposes
        //transform.position = startPositions[Random.Range(0, startPositions.Count)];
    }

    public void MakeObjActive(GameObject obj) {
        if(!obj.activeSelf) {
            obj.SetActive(true);
        }
    }

    public void MakeObjDeactive(GameObject obj) {
        if(obj.activeSelf) {
            obj.SetActive(false);
        }
    }

    public override void OnDeath() {
        SceneManager.LoadScene(1);
    }

    new void Update()
    {
        // Skilltree
        if(Input.GetKeyDown(KeyCode.T)) {
            if(!skilltree.activeSelf) {
                skilltree.SetActive(true);
                skilltree.transform.position = transform.position;
                GetComponent<PlayerMovement>().isPaused = true;
            }
            else {
                skilltree.SetActive(false);
                GetComponent<PlayerMovement>().isPaused = false;
            }
        }

        // Handling factories
        if(currentFactory) {
            MakeObjActive(factoryOverview);
            factoryOverview.GetComponent<FactoryManager>().SetFactory(currentFactory);
            if(currentFactory.CanBuild()) {
                if(Input.GetKeyDown(KeyCode.B)) {
                    currentFactory.Build();
                    MakeObjDeactive(bObject);
                }
            }
            if(currentFactory.CanAutoInfect()) {
                if(Input.GetKeyDown(KeyCode.I)) {
                    currentFactory.AutoInfect();
                    MakeObjDeactive(iObject);
                }
            }
            if(Input.GetKeyDown(KeyCode.F)) {
                currentFactory.SendBacteriasToPlayer();
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
                    currentCell.GetAbsorbed();
                }
                else if(Input.GetKeyDown(KeyCode.Q)) {
                    currentCell.TurnIntoFactory();
                }
            }
            else {
                MakeObjActive(spaceObject);
                if(Input.GetKey(KeyCode.Space)) {
                    currentCell.Infect(Time.deltaTime);
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
