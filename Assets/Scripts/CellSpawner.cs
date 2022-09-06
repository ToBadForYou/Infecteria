using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject cellObject;
    public Transform playerPosition;
    public int distance = 5;
    public int amountX = 20;
    public int amountY = 20;
    // Start is called before the first frame update
    void Start()
    {
        float scale = cellObject.GetComponent<Transform>().localScale.x;
        float playerX = playerPosition.position.x;
        float playerY = playerPosition.position.y;
        int startX = amountX/2;
        int startY = amountY/2;
        for (int x = -startX; x < startX; x++)
        {
            for (int y = -startY; y < startY; y++)
            {
                Instantiate(cellObject, new Vector2(playerX + scale * x * distance , playerY + scale * y * distance), Quaternion.identity);
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
