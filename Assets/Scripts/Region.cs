using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region
{
    public Vector2 xRange;
    public Vector2 yRange;

    public Region(float startX, float endX, float startY, float endY) {
        xRange = new Vector2(startX, endX);
        yRange = new Vector2(startY, endY);
    }

}
