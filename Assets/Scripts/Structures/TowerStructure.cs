using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStructure : Structure
{

    void Start(){
        buildable = GameObject.Find("Tower").GetComponent<Buildable>();
    }

}
