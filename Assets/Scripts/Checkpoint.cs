using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    int health = 3;
    List<GameObject> nodes = new List<GameObject>();
    int totalHeight;
    UnitSpawner unitSpawner;

    void Start(){
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
    }

    public void InitPatrol(int height){
        unitSpawner = GameObject.Find("UnitSpawner").GetComponent<UnitSpawner>();
        totalHeight = height;
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        // TODO: Patrols can respawn and alert nearby nodes, add scout unit
        List<Unit> antibodies = unitSpawner.SpawnAntibodies(transform.position, 2);
        UnitSquad newSquad = unitSpawner.CreateSquad(antibodies);
        newSquad.Patrol(new Vector2(xPos + 1.5f, yPos + totalHeight/2), new Vector2(xPos + 1.5f, yPos - totalHeight/2), false);

        List<Unit> antibodies2 = unitSpawner.SpawnAntibodies(transform.position, 2);
        UnitSquad newSquad2 = unitSpawner.CreateSquad(antibodies2);
        newSquad2.Patrol(new Vector2(xPos - 1.5f, yPos - totalHeight/2), new Vector2(xPos - 1.5f, yPos + totalHeight/2), false);        
    }

    public void AddNode(GameObject node){
        nodes.Add(node);
    }

    public void RemoveNode(GameObject node){
        nodes.Remove(node);
    }

    public void TakeDamage(){
        health -= 1;
        if (health < 1){
            foreach (GameObject node in nodes){
                Destroy(node);
            }
            GetComponent<AbsorbEffect>().isActive = true;
        }
    }
}
