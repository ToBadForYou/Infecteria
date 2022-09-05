using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Cell currentCell;

    public GameObject spaceObject;
    public GameObject eObject;
    public GameObject qObject;

    void Start()
    {
        
    }

    void MakeObjActive(GameObject obj) {
        if(!obj.activeSelf) {
            obj.SetActive(true);
        }
    }

    void MakeObjDeactive(GameObject obj) {
        if(obj.activeSelf) {
            obj.SetActive(false);
        }
    }

    void Update()
    {
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
                    Debug.Log("Infecting");
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
