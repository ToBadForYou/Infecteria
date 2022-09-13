using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGenerator : MonoBehaviour
{
    public GameObject wallObj;
    public GameObject regionObj;
    public Texture2D img;

    private int width = 399;
    private int xCounter = 0;
    private int yCounter = 0;

    Transform topLeft;
    Transform topRight;
    Transform bottomLeft;
    Transform bottomRight;

    void Start()
    {
        Color[] pixels = img.GetPixels();
        foreach (Color color in pixels)
        {
            xCounter++;
            
            if(color.Equals(Color.black)) {
                Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);
            }
            else if(color.Equals(Color.green)) {
                GameObject temp = Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);

                if(!bottomLeft) {
                    temp.name = "BottomLeft";
                    bottomLeft = temp.transform;
                }
                else if(!bottomRight) {
                    temp.name = "BottomRight";
                    bottomRight = temp.transform;
                }
                else if(!topLeft) {
                    temp.name = "TopLeft";
                    topLeft = temp.transform;
                }
                else {
                    temp.name = "TopRight";
                    topRight = temp.transform;

                    GameObject region = Instantiate(regionObj);

                    topLeft.parent = region.transform;
                    topLeft = null;
                    topRight.parent = region.transform;
                    topRight = null;
                    bottomLeft.parent = region.transform;
                    bottomLeft = null;
                    bottomRight.parent = region.transform;
                    bottomRight = null;
                }

                temp.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f);
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
