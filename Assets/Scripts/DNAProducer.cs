using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAProducer : MonoBehaviour
{
    public GameManager gameManager;
    public int baseProduction = 10;
    public int baseTimer = 5;
    public float productionTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        productionTimer -= Time.deltaTime;
        if (productionTimer < 0){
            gameManager.DNAPoints += baseProduction;
            productionTimer = baseTimer;
        }
    }
}
