using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool walkable = true;
	public bool current = false;

	public Tile tileUp = null;
	public Tile tileDown = null;
	public Tile tileLeft = null;
	public Tile tileRight = null;

	//public List<Tile> adjacencyList = new List<Tile>();



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reset () 
	{
		//adjacencyList.Clear();
		current = false;
		tileUp = null;
		tileDown = null;
		tileLeft = null;
		tileRight = null;
		//visited = false;
		//parent = null;
		//distance = 0;
	}

	public void FindNeighbors (float jumpheight) {
		Reset ();
		//up
		tileUp = CheckTile (Vector3.forward, jumpheight);
		//down
		tileDown = CheckTile (-Vector3.forward, jumpheight);
		//right
		tileRight = CheckTile(Vector3.right, jumpheight);
		//left
		tileLeft = CheckTile(-Vector3.right, jumpheight);

	}

	public Tile CheckTile (Vector3 direction, float jumpheight) 
	{
		Vector3 halfExtents = new Vector3 (0.25f, (1 + jumpheight) / 2.0f, 0.25f);
		Collider[] colliders = Physics.OverlapBox (transform.position + direction, halfExtents);

		foreach (Collider item in colliders) 
		{
			Tile tile = item.GetComponent<Tile> ();
			if (tile != null && tile.walkable) 
			{
				RaycastHit hit;
				if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1)) 
				{
					//adjacencyList.Add(tile);
					return tile;
				}

			}
		}
		return null;
	}
}
