using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Cell currentCell;

    void Start()
    {
        
    }

    void Update()
    {
        if(currentCell.isInfected) {
            if(Input.GetKeyDown(KeyCode.E)) {
                currentCell.GetAbsorbed();
            }
            else if(Input.GetKeyDown(KeyCode.I)) {
                currentCell.TurnIntoFactory();
            }
        }
        else {
            if(Input.GetKey(KeyCode.Space)) {
                currentCell.Infect(Time.deltaTime);
            }
        }
    }
}
