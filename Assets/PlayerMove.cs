using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : GridMovement 
{

	// Use this for initialization
	void Start () 
	{
		Init(); 
	}

	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, transform.forward, Color.red);

		if (!turn) 
		{
			return;
		}

		if (!moving) 
		{
			FindPossibleTiles ();
			GetInput();

		} 
		else 
		{
			Move();
		}

	}

	void GetInput() {
		if (Input.GetKeyDown("up") && currentTile.tileUp != null)
			StartMove(Vector3.forward, currentTile.tileUp);
    
		if (Input.GetKeyDown("down") && currentTile.tileDown != null)
			StartMove(-Vector3.forward, currentTile.tileDown);

		if (Input.GetKeyDown("left") && currentTile.tileLeft != null)
			StartMove(-Vector3.right, currentTile.tileLeft);
	    
		if (Input.GetKeyDown("right") && currentTile.tileRight != null)
			StartMove(Vector3.right, currentTile.tileRight);
	}
}
