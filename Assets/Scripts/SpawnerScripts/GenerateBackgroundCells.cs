using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBackgroundCells : MonoBehaviour
{
    public GameObject backgroundCellObj;
    public float scale;

    void Start()
    {
        for(int i = 0; i < 10; i++) {
            GameObject temp = Instantiate(backgroundCellObj, new Vector2(Random.Range(-40.0f, 40.0f), Random.Range(-40.0f, 40.0f)), Quaternion.identity);
            temp.transform.localScale = new Vector2(scale, scale);
            temp.transform.parent = transform;
        }
    }
}
