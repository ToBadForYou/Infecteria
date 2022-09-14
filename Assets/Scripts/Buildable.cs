using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public GameObject structure;
    public int cost;

    Sprite GetSprite(){
        return structure.GetComponent<SpriteRenderer>().sprite;
    }

    Color GetColor(){
        return structure.GetComponent<SpriteRenderer>().color;
    }
}
