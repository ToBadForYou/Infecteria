using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGenerator : MonoBehaviour
{
    public GameObject wallObj;
    public Texture2D img;

    private int width = 399;
    private int xCounter = 0;
    private int yCounter = 0;

    void Start()
    {
        Color[] pixels = img.GetPixels();
        foreach (Color color in pixels)
        {
            xCounter++;
            
            if(color.Equals(Color.black)) {
                Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);
            }
            else {

            }

            if(xCounter == width) {
                yCounter++;
                xCounter = 0;
            }
        }
    }
}
