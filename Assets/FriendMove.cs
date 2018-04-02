using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : GridMovement 
{

	public bool needToMove = false;

	// Use this for initialization
	void Start () 
	{
		Init(); 
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.DrawRay (transform.position, transform.forward, Color.red);

		if (!turn) 
		{
			return;
		}

		if (!moving) 
		{
			FindPossibleTiles();
			needToMove = CheckForPlayer();
			print(needToMove);
			if (needToMove) 
			{
				moving = true;
			} 
			else 
			{
				moving = false;
				TurnManager.EndTurn();
			}
		}
		else 
		{
			print("next to player!");

		}



		//if (!moving) 
		//{
		//	FindPossibleTiles ();
		//	GetInput();

		//} 
		//else 
		//{
		//	Move();
		//}

	}

	bool CheckForPlayer () 
	{
		Tile thisTile;
		thisTile = currentTile.tileDown;
		if (currentTile.tileDown != null) 
		{
			print(currentTile.tileDown.occupied);
			if(thisTile.occupied) 
			{
				print("down tile occupied");
			}

		}



		if (currentTile.tileUp != null && currentTile.tileUp.occupied) 
		{
			return true;
		} 
		else if (currentTile.tileDown != null && currentTile.tileDown.occupied) 
		{
			return true;
		} 
		else if (currentTile.tileLeft != null && currentTile.tileLeft.occupied) 
		{
			return true;
		} 
		else if (currentTile.tileRight != null && currentTile.tileRight.occupied) 
		{
			return true;
		} 
		else 
		{
			return false;
		}

	}


}
