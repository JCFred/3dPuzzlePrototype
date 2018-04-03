using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour 
{
	public bool turn = false;
	
	GameObject[] tiles;

	public bool moving = false;
	public float jumpHeight = 2;
	public float moveSpeed = 2;

	public Tile currentTile;

	Vector3 velocity = new Vector3();
	//Vector3 heading = new Vector3();
	Vector3 targetPosition = new Vector3();

	public void Init() 
	{
		tiles = GameObject.FindGameObjectsWithTag("tile");

		//add this unit to the teams list
		TurnManager.AddUnit(this);
	}

	public void GetCurrentTile () 
	{
		currentTile = GetTargetTile(gameObject);
		currentTile.current = true;
	}

	public Tile GetTargetTile (GameObject target)
	{
		RaycastHit hit;
		Tile tile = null;

		if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)) 
		{
			tile = hit.collider.GetComponent<Tile>();
		}

		return tile;
	}

	public void FindPossibleTiles() 
	{
		ComputeAdjacents ();
		GetCurrentTile ();
	}

	public void ComputeAdjacents () 
	{
		foreach (GameObject tile in tiles) 
		{
			Tile t = tile.GetComponent<Tile>();
			t.FindNeighbors(jumpHeight);
		}
	}

	public void StartMove (Vector3 direction, Tile target) 
	{
		targetPosition = target.transform.position;
		targetPosition.y += 1.4f;
		SetHorizontalVelocity (direction);
		transform.forward = direction;
		moving = true;

		

	}

	public void Move () 
	{
		if (Vector3.Distance (transform.position, targetPosition) >= 0.09f) 
		{
			transform.position += velocity * Time.deltaTime;
		} 
		else 
		{
			transform.position = targetPosition;
			moving = false;
			TurnManager.EndTurn();
		}
	}

	//void CalculateHeading (Vector3 target) 
	//{
	//	heading = target - transform.position;
	//	heading.Normalize();
	//}

	void SetHorizontalVelocity (Vector3 direction) 
	{
		velocity = direction * moveSpeed;

	}

	public void BeginTurn () 
	{
		turn = true;
	}

	public void EndTurn () 
	{
		turn = false;
	}

}

