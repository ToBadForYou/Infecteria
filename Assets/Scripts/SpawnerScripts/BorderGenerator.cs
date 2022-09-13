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

    GameObject lastObj;

    void Start()
    {
        Color[] pixels = img.GetPixels();
        foreach (Color color in pixels)
        {
            xCounter++;
            
            if(color.Equals(Color.black)) {

                if(lastObj) { // Pixel before this was black
                    if(lastObj.transform.position.y == yCounter-200) { // Y level is same so optimize
                        float lastX = lastObj.transform.position.x; // Get last x position
                        float lastScale = lastObj.transform.localScale.x; // Get last x scale
                        Destroy(lastObj); // Destroy last object
                        lastObj = Instantiate(wallObj, new Vector2(lastX, yCounter-200), Quaternion.identity); // Put new object where last object was
                        lastObj.transform.localScale = new Vector2(lastObj.transform.localScale.x + lastScale, lastObj.transform.localScale.y);
                        lastObj.transform.position = new Vector2(lastObj.transform.position.x + 0.5f, lastObj.transform.position.y);
                    } // TO THINK ABOUT: Same X level is more tricky since they does not get placed directly after each other
                    else{
                        lastObj = Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);
                    }
                }
                else {
                    lastObj = Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);
                }
                lastObj.name = "BorderObj";
                lastObj.transform.parent = transform;

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
                lastObj = null;
            }
            else {
                lastObj = null;
            }

            if(xCounter == width) {
                yCounter++;
                xCounter = 0;
            }
        }
    }
}
