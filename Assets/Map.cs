using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    /// <summary>
    /// The dimensions of this map in number of tiles.
    /// </summary>
    public int MapHeight, MapWidth;

    public int mapOriginX, mapOriginY;

    public MapTile tilePrefab;



    /// <summary>
    /// This will hold all the map tiles in this map.
    /// </summary>
    private List<MapTile> tileList;


	// Use this for initialization
	void Start () {

        //Instatiate the list
        tileList = new List<MapTile>();

        BuildMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void BuildMap()
    {

        float tileWidth = 0;
        float tileHeight = 0;


        if (MapHeight <= 0) Debug.LogWarning("Cannot create a map with 0 length. Check object " + gameObject.name);
        if (MapWidth <= 0) Debug.LogWarning("Cannot create a map with 0 width. Check object " + gameObject.name);

        for (int i = 1; i <= MapHeight; i++)
        {
            for(int j = 1; j <= MapWidth; j++)
            {
                //Generate the tile
                MapTile mt = Instantiate<MapTile>(tilePrefab);
                
                //we need the width of the tile to get the offsets, so we get it just once
                if(tileWidth == 0)
                {
                    SpriteRenderer sr = mt.GetComponent<SpriteRenderer>();
                    if (sr == null) Debug.LogWarning("MapTile object missing a SpriteRenderer Component.");
                    tileWidth = sr.bounds.size.x;
                }
                if (tileHeight == 0)
                {
                    SpriteRenderer sr = mt.GetComponent<SpriteRenderer>();
                    if (sr == null) Debug.LogWarning("MapTile object missing a SpriteRenderer Component.");
                    tileHeight = sr.bounds.size.y;
                }

                //Set the coordinates local to the tile
                mt.gridPosition.x = i;
                mt.gridPosition.y = j;

                //Set the map object as the parent of the tile
                mt.gameObject.transform.parent = transform;

                //set the position of the tile
                mt.gameObject.transform.position = new Vector2(mapOriginX + (tileWidth * i), mapOriginY + (tileHeight * j));

                //add it to the list of tiles held here
                tileList.Add(mt);
            }
        }
    }



}
