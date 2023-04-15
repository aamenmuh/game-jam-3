using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrapGen : MonoBehaviour
{
    public Tilemap tilemap;
    public List<Vector3> tileWorldLocations;
    public int maxScrap;
    public GameObject scrapMound;

    //Called by another script (nonexistent) whenever scrap needs to be generated
    public void Generate()
    {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
            }
        }

        maxScrap = Mathf.Clamp(maxScrap, 0, tileWorldLocations.Count);
        Random.seed = System.DateTime.Now.Millisecond;
        for(int i = 0; i < maxScrap; i++)
        {
            int itemIndex = Random.Range(0, tileWorldLocations.Count);
            Instantiate(scrapMound, new Vector3(tileWorldLocations[itemIndex].x + 0.5f, tileWorldLocations[itemIndex].y + 0.5f, tileWorldLocations[itemIndex].z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Testing purposes
        if (Input.GetKeyDown(KeyCode.G)) Generate();
    }
}
