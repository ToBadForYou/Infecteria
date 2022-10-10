using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject scoutObject;
    public GameObject antibodyObject;

    public Scout SpawnScout(Vector2 pos, GameObject parent){
        GameObject newScout = Instantiate(scoutObject, pos, Quaternion.identity);
        Scout scout = newScout.transform.Find("scoutRange").gameObject.GetComponent<Scout>();
        scout.parent = parent;
        return scout;
    }

    public List<Unit> SpawnAntibodies(Vector2 pos, int amount){
        List<Unit> antibodies = new List<Unit>();
        for (int i = 0; i < amount; i++)
        {
            GameObject newAntibody = Instantiate(antibodyObject, new Vector2(pos.x + Random.Range(-1.0f, 1.0f), pos.y + Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            Unit antibodyUnit = newAntibody.GetComponent<Unit>();
            antibodies.Add(antibodyUnit);
        }
        return antibodies;
    }

    public UnitSquad CreateSquad(List<Unit> units){
        GameObject tempObj = new GameObject("UnitSquad");
        UnitSquad newUnitSquad = tempObj.AddComponent<UnitSquad>();
        newUnitSquad.AddUnits(units);
        return newUnitSquad;
    }   
}
