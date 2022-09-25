using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarProducer : MonoBehaviour
{
    public GameManager gameManager;
    public int baseProduction = 10;
    public int baseTimer = 5;
    public float productionTimer = 5;

    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        productionTimer -= Time.deltaTime;
        if (productionTimer < 0){
            gameManager.IncreaseSugar(baseProduction);
            productionTimer = baseTimer;
        }
    }
}
