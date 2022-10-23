using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    Region region1;
    Region region2;
    Region region3;
    Region region4;

    public List<GameObject> backgroundObjs;
    public int objectAmount = 800;

    bool IsOutsideRegions(Vector2 pos){
        return !region1.IsInside(pos) && !region2.IsInside(pos) && !region3.IsInside(pos) && !region4.IsInside(pos);
    }

    void Start(){
        region1 = new Region(-135.0f, -35.0f, 106.0f, 160.0f);
        region2 = new Region(35.0f, 135.0f, 106.0f, 160.0f);
        region3 = new Region(-135.0f, -35.0f, -26.0f, 50.0f);
        region4 = new Region(35.0f, 135.0f, -26.0f, 50.0f);

        for(int i = 0; i < objectAmount; i++) {
            Vector2 position = new Vector2(Random.Range(-135.0f, 135.0f), Random.Range(-85.0f, 160.0f));
            if(IsOutsideRegions(position)) {
                GameObject obj = Instantiate(backgroundObjs[Random.Range(0, backgroundObjs.Count)], position, Quaternion.identity);
                float bValue = (obj.transform.position.x - (-135.0f))/(160.0f - (-135.0f));
                float gValue = (obj.transform.position.y - (-135.0f))/(160.0f - (-135.0f));
                obj.GetComponent<SpriteRenderer>().color = new Color(0.3921569f, gValue/4.0f, bValue/4.0f);
            }
        }
    }
}
