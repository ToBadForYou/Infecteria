using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : RangedUnit
{
    public GameObject reportTo;
    public GameObject scoutObject;
    public bool isAlerted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Attack(){
        if (!isAlerted && target.tag == "Player"){
            isAlerted = true;
            SpawnScout();
        }
    }

    void SpawnScout(){
        GameObject newScout = Instantiate(scoutObject, transform.position, Quaternion.identity);
        Scout scout = newScout.GetComponent<Scout>();
        scout.parent = reportTo;
        scout.SetAlerted();
    }

    void StopAttack(){
        
    }
}
