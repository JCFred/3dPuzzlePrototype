using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : GridMovement 
{
	int playerPos = 0;
	int LEFT = 1;
	int UP = 2;
	int RIGHT = 3;
	int DOWN = 4;


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
			SimpleRunAway();
			Move();
		}


	}

	bool CheckForPlayer () 
	{
		if (currentTile.tileLeft != null && currentTile.tileLeft.occupied) 
		{
			playerPos = LEFT;
			return true;
		} 
		else if (currentTile.tileUp != null && currentTile.tileUp.occupied) 
		{
			playerPos = UP;
			return true;
		} 
		else if (currentTile.tileRight != null && currentTile.tileRight.occupied) 
		{
			playerPos = RIGHT;
			return true;
		} 
		else if (currentTile.tileDown != null && currentTile.tileDown.occupied) 
		{
			playerPos = DOWN;
			return true;
		} 
		else 
		{
			playerPos = 0;
			return false;
		}

	}

	void SimpleRunAway () 
	{
		if (playerPos == LEFT) 
		{
			//check right for open tile
			if (currentTile.tileRight != null && currentTile.tileRight.walkable) 
			{
				//move right
				StartMove (Vector3.right, currentTile.tileRight);
			} 
			else 
			{
				//complex move up or down
				ComplexMoveUD ();
			}
		} 
		else if (playerPos == UP) 
		{
			//check down for open tile
			if (currentTile.tileDown != null && currentTile.tileDown.walkable) 
			{
				//move down
				StartMove (-Vector3.forward, currentTile.tileDown);
			} 
			else 
			{
				//complex move left or right
				ComplexMoveLR ();
			}
		} 
		else if (playerPos == RIGHT) 
		{
			//check left for open tile
			if (currentTile.tileLeft != null && currentTile.tileLeft.walkable) 
			{
				//move left
				StartMove (-Vector3.right, currentTile.tileLeft);
			} 
			else 
			{
				//comple move up or down
				ComplexMoveUD ();
			}
		} 
		else if (playerPos == DOWN) 
		{
			//check up for open tile
			if (currentTile.tileUp != null && currentTile.tileUp.walkable) 
			{
				//move up
				StartMove (Vector3.forward, currentTile.tileUp);
			} 
			else 
			{
				//complex move left or right
				ComplexMoveLR ();
			}
		}
	}

	void ComplexMoveUD () 
	{
		//check is up tile is unavailable 
		if (currentTile.tileUp == null || !currentTile.tileUp.walkable) 
		{
			//move down
			StartMove (-Vector3.forward, currentTile.tileDown);
		} 
		//check if down tile is unavailable 
		else if (currentTile.tileDown == null || !currentTile.tileDown.walkable) 
		{
			//move up
			StartMove (Vector3.forward, currentTile.tileUp);
		}
		//both tiles are available
		else if ((currentTile.tileUp != null && currentTile.tileUp.walkable) && (currentTile.tileDown != null && currentTile.tileDown.walkable)) 
		{
			//count which of the tiles has the most options for future movement
			int upPossible = countConnections (currentTile.tileUp);
			int downPossible = countConnections (currentTile.tileDown);

			//more possible Up
			if (upPossible > downPossible) 
			{
				//move up
				StartMove (Vector3.forward, currentTile.tileUp);
			} 
			//more down possible
			else if (downPossible > upPossible) 
			{
				//move down
				StartMove (-Vector3.forward, currentTile.tileDown);
			}
			//up and down have equal number of possible moves
			else 
			{
				//choose one ranomsly?
			}


		}
	}

	void ComplexMoveLR () 
	{
		//check is left tile is unavailable 
		if (currentTile.tileLeft == null || !currentTile.tileLeft.walkable) 
		{
			//move right
			StartMove (Vector3.right, currentTile.tileRight);
		} 
		//check if right tile is unavailable 
		else if (currentTile.tileRight == null || !currentTile.tileRight.walkable) 
		{
			//move left
			StartMove (-Vector3.right, currentTile.tileLeft);
		}
		//both tiles are available
		else if ((currentTile.tileRight != null && currentTile.tileRight.walkable) && (currentTile.tileLeft != null && currentTile.tileLeft.walkable)) 
		{
			//count which of the tiles has the most options for future movement
			int leftPossible = countConnections (currentTile.tileLeft);
			int rightPossible = countConnections (currentTile.tileRight);

			//more possible left
			if (leftPossible > rightPossible) 
			{
				//move left
				StartMove (-Vector3.right, currentTile.tileLeft);
			} 
			//more right possible
			else if (rightPossible > leftPossible) 
			{
				//move right
				StartMove (Vector3.right, currentTile.tileRight);
			}
			//left and right have equal number of possible moves
			else 
			{
				//choose one ranomsly?
			}


		}
	}

	public int countConnections (Tile tile) 
	{
		int count = 0;
		if(tile.tileLeft != null) 
		{
			count += 1;
		}
		if(tile.tileUp != null) 
		{
			count += 1;
		}
		if(tile.tileRight != null) 
		{
			count += 1;
		}
		if(tile.tileDown != null) 
		{
			count += 1;
		}

		return count;
	}

}
