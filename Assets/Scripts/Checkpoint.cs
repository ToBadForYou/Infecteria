using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    int health = 3;
    List<GameObject> nodes = new List<GameObject>();

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
