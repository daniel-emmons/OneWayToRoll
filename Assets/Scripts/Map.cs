using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    /// <summary>
    /// The dimensions of this map in number of tiles.
    /// </summary>
    public int MapHeight, MapWidth;

    public int mapOriginOffsetX, mapOriginOffsetY;

    public MapTile tilePrefab;

    public Vector2 playerStartPosition = new Vector2(0,0);


    /// <summary>
    /// This will hold all the map tiles in this map.
    /// </summary>
    private List<MapTile> tileList;


	// Use this for initialization
	void Start () {


	}

    void Awake()
    {
        //Instatiate the list
        tileList = new List<MapTile>();

        BuildMap();

        CreatePlayer();
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

        for (int i = 1; i <= MapWidth; i++)
        {
            for (int j = 1; j <= MapHeight; j++)
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
                mt.gameObject.transform.position = new Vector2(gameObject.transform.position.x + mapOriginOffsetX + (tileHeight * i), gameObject.transform.position.y + mapOriginOffsetY - (tileWidth * j));
                //mt.gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                //add it to the list of tiles held here
                tileList.Add(mt);
            }
        }
    }

    void CreatePlayer()
    {
        GetTile(3, 1);
    }

    public void SpawnPlayer(Player playerObject)
    {
        MapTile mt = GetTile(playerStartPosition);
        Player player = Instantiate<Player>(playerObject);

        player.gameObject.transform.position = mt.gameObject.transform.position;

        ///Setting references of the tile to the player.
        mt.occupyingUnit = player;

        ///Setting references in the player to where it now exists.
        player.currentMap = this;
        player.currentTile = mt;

    }

    MapTile GetTile(float xCoord, float yCoord)
    {
        Vector2 requestedCoordinates = new Vector2(xCoord, yCoord);
        MapTile mt = tileList.Find(item => item.gridPosition == requestedCoordinates);
        return mt;
    }

    MapTile GetTile(Vector2 playerPosition)
    {
        MapTile mt = tileList.Find(item => item.gridPosition == playerPosition);
        return mt;
    }

}
