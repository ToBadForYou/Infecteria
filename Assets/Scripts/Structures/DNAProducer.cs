using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAProducer : MonoBehaviour
{
    public Transform barPivot;

    public GameManager gameManager;
    public int baseProduction = 1;
    public int baseTimer = 5;
    public float productionTimer = 5;

    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        if(PauseManager.Instance.CurrPauseState == PauseManager.PauseState.NONE) {
            productionTimer -= Time.deltaTime;

            float progress = productionTimer/(float)baseTimer;
            barPivot.localScale = new Vector2(1.0f - progress, 1.0f);
            if (productionTimer < 0){
                barPivot.localScale = new Vector2(0.0f, 1.0f);
                gameManager.IncreaseDNAPoints(baseProduction);
                productionTimer = baseTimer;
            }
        }
    }
}
