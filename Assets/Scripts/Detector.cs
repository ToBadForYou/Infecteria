using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : RangedUnit
{
    public GameObject reportTo;
    public GameObject scoutObject;
    public GameObject antibodyObject;
    public UnitSquad unitSquad;
    public bool isAlerted;
    public int maxAntibodies = 3;

    // Start is called before the first frame update
    void Start()
    {
        reportTo = GameObject.Find("Heart");
    }

    public override void Attack(){
        if (!isAlerted && target.tag == "Player"){
            isAlerted = true;
            SpawnUnits();
        }
    }

    void SpawnUnits(){
        GameObject newScout = Instantiate(scoutObject, transform.position, Quaternion.identity);
        Scout scout = newScout.transform.Find("scoutRange").gameObject.GetComponent<Scout>();
        scout.parent = reportTo;
        scout.SetAlerted(gameObject);

        int antibodiesAmount = Random.Range(2, maxAntibodies);
        for (int i = 0; i < antibodiesAmount; i++)
        {
            GameObject newAntibody = Instantiate(antibodyObject, new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            unitSquad.AddUnit(newAntibody.GetComponent<Unit>());
        }
    }

    void StopAttack(){
        
    }
}
