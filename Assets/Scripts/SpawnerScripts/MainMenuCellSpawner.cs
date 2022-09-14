using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCellSpawner : MonoBehaviour
{
    public float maxY;
    public float maxX;
    public int cellAmount;
    public GameObject mainMenuCellObj;

    GameObject temp;

    void Start()
    {
        for(int i = 0; i < cellAmount; i++) {
            Vector2 pos = GetRandomVector();

            if(temp) {
                while(!IsValidPosition(pos)) {
                    pos = GetRandomVector();
                }
            }

            temp = Instantiate(mainMenuCellObj, pos, Quaternion.identity);
        }
    }

    bool IsValidPosition(Vector2 pos) {
        float distance = Vector2.Distance(temp.transform.position, pos);
        if(distance <= 5.0f) {
            return false;
        }
        return true;
    }

    Vector2 GetRandomVector() {
        return new Vector2(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY));
    }
}
