using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public GameObject structure;
    public string structureName;
    public int cost;

    public Sprite GetSprite(){
        return structure.GetComponent<SpriteRenderer>().sprite;
    }

    public Color GetColor(){
        return structure.GetComponent<SpriteRenderer>().color;
    }
}
