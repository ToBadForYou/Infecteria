using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Cell currentCell;
    public Factory currentFactory;

    public GameObject spaceObject;
    public GameObject eObject;
    public GameObject qObject;
    public GameObject bObject;

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

    void Update()
    {
        // Handling factories
        if(currentFactory) {
            if(Input.GetKeyDown(KeyCode.B)) {
                currentFactory.Build();
            }
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
