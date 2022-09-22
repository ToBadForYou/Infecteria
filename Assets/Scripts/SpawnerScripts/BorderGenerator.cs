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

    Dictionary<float, List<GameObject>> xLines = new Dictionary<float, List<GameObject>>();

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
                    }
                    else{
                        lastObj = Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);
                    }
                }
                else {
                    lastObj = Instantiate(wallObj, new Vector2(xCounter-200, yCounter-200), Quaternion.identity);
                    if(xLines.ContainsKey(xCounter-200)) {
                        xLines[xCounter-200].Add(lastObj);
                    }
                    else {
                        List<GameObject> objs = new List<GameObject>();
                        objs.Add(lastObj);
                        xLines.Add(xCounter-200, objs);
                    }
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
        HandleXLines();
    }

    void HandleXLines() {
        foreach(KeyValuePair<float, List<GameObject>> entry in xLines) {
            int len = entry.Value.Count;
            if(len < 5) { // Skipping non-lines
                continue;
            }
            else {
                GameObject last = null;
                int counter = 0;
                
                foreach(GameObject obj in entry.Value) {
                    if(!last) {
                        last = obj;
                        continue;
                    }
                    if(counter == len-1) {
                        continue;
                    }
                    float lastY = last.transform.position.y; // Get last y position
                    float lastScale = last.transform.localScale.y; // Get last y scale
                    Destroy(last); // Destroy last object
                    Destroy(obj); // Destroy last object
                    last = Instantiate(wallObj, new Vector2(entry.Key, lastY), Quaternion.identity); // Put new object where last object was
                    last.transform.localScale = new Vector2(last.transform.localScale.x, last.transform.localScale.y + lastScale);
                    last.transform.position = new Vector2(last.transform.position.x, last.transform.position.y + 0.5f);
                    counter++;
                }
            }
        }
    }
}
