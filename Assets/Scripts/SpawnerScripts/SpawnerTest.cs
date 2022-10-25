using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    public GameObject cellObj;
    public GameObject detectorObj;
    public int step = 10;

    void Start(){
        Transform topLeft = transform.GetChild(0);
        Transform topRight = transform.GetChild(1);
        Transform bottomLeft = transform.GetChild(2);
        Transform bottomRight = transform.GetChild(3);

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        for(int x = (int)topLeft.position.x + step/2; x < (int)topRight.position.x; x+=step) {
            for(int y = (int)topLeft.position.y - step/2; y > (int)bottomRight.position.y; y-=step) {
                GameObject cell = Instantiate(cellObj, new Vector2(x, y), Quaternion.identity);
                gm.AddCell(cell);
            }
        }

        for(int x = (int)topLeft.position.x + step; x < (int)topRight.position.x; x+=step*4) {
            for(int y = (int)topLeft.position.y - step; y > (int)bottomRight.position.y; y-=step*4) {
                Instantiate(detectorObj, new Vector2(x + Random.Range(-5.0f, 5.0f), y + Random.Range(-5.0f, 5.0f)), Quaternion.identity);
            }
        }
    }
}
