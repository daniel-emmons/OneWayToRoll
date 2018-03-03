using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

    public Vector2 gridPosition;

    public TileType tileType;

    public Unit occupyingUnit;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum TileType
{
    Plains,
    Mountain,
    Wall,
    Water,
}
