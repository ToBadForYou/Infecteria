using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject cellObject;
    public GameObject heartObject;
    public int distance = 5;
    public int amountX = 20;
    public int amountY = 20;
    
    void Start()
    {
        float scale = cellObject.GetComponent<Transform>().localScale.x;
        Heart heart = heartObject.GetComponent<Heart>();
        int startX = amountX/2;
        int startY = amountY/2;
        for (int x = -startX; x < startX; x++)
        {
            for (int y = -startY; y < startY; y++)
            {
                GameObject cell = Instantiate(cellObject, new Vector2(transform.position.x + scale * x * distance , transform.position.y + scale * y * distance), Quaternion.identity);
                heart.AddCell(cell);
            }
        }        
    }

}
