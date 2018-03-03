using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// If there is a map here, then automatically load it into the scene on startup.
    /// </summary>
    public Map debugMapToLoad;

    public Player playerObject;


    //The active map we're using. Should only be one here.
    private Map activeMap;

	// Use this for initialization
	void Start () {

        LoadMap();
        
        //After we've figured out what map we're doing, now we can set up the map
        SetUpMap();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadMap()
    {
        //Easy loading of maps used for testing
        if (Debug.isDebugBuild && debugMapToLoad != null) CreateDebugMap();

        else Debug.LogWarning("No map has been loaded.");
    }

    void CreateDebugMap()
    {
        Map m = Instantiate<Map>(debugMapToLoad);
        activeMap = m;
    }

    void SetUpMap()
    {
        activeMap.SpawnPlayer(playerObject);
    }


}
