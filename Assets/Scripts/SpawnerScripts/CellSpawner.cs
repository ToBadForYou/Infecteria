using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    public GameObject cellObject;
    public GameObject detectorObject;

    public int distance = 5;
    public int amountX = 2;
    public int amountY = 2;
    
    void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Vector2 playerPosition = GameObject.Find("Player").transform.position;

        float scale = cellObject.GetComponent<Transform>().localScale.x;
        int startX = amountX/2;
        int startY = amountY/2;

        for (int x = -startX; x < startX; x++)
        {
            for (int y = -startY; y < startY; y++)
            {
                GameObject cell = Instantiate(cellObject, new Vector2(transform.position.x + scale * x * distance , transform.position.y + scale * y * distance), Quaternion.identity);
                gm.AddCell(cell);
            }
        }

        for (int x = -startX + 2; x < startX - 2; x+=6)
        {
            for (int y = -startY + 2; y < startY - 2; y+=4)
            {
                float xPos = transform.position.x + scale * x * distance + Random.Range(-5, 5);
                float yPos = transform.position.y + scale * y * distance + Random.Range(-5, 5);
                Vector2 position = new Vector2(xPos, yPos);

                if(Vector2.Distance(playerPosition, position) > 3.5f) { // In order to avoid spawning in detector
                    Instantiate(detectorObject, position, Quaternion.identity);
                }
            }
        }             
    }

}
