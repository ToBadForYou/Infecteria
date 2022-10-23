using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public List<Region> spawnableRegion = new List<Region>();
    Region activeRegion;

    void Start(){
        //TODO: Do this automatically in BorderGenerator later
        Region leftArmRegion = new Region(-133, -50, 51, 104);
        spawnableRegion.Add(leftArmRegion);
        Region rightArmRegion = new Region(50, 133, 51, 104);
        spawnableRegion.Add(rightArmRegion);
        Region leftLegRegion = new Region(-133, -50, -82, -29);
        spawnableRegion.Add(leftLegRegion);
        Region rightLegRegion = new Region(50, 133, -82, -29);
        spawnableRegion.Add(rightLegRegion);

        /* old spawnable regions, good for debugging
        Region region1 = new Region(-133, 133, 51, 104); 
        spawnableRegion.Add(region1);
        Region region2 = new Region(-133, 133, -82, -29);
        spawnableRegion.Add(region2);
        */

        activeRegion = spawnableRegion[Random.Range(0, spawnableRegion.Count)];
        
        float xMin = activeRegion.xRange.x;
        float xMax = activeRegion.xRange.y;
        float xPosition = Random.Range(xMin, xMax);

        float yMin = activeRegion.yRange.x;
        float yMax = activeRegion.yRange.y;
        float yPosition = Random.Range(yMin, yMax);

        transform.position = new Vector2(xPosition, yPosition);
        Camera.main.transform.position = new Vector2(xPosition, yPosition);
    }
}
