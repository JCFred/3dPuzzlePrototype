using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
	static Dictionary<string, List<GridMovement>> units = new Dictionary<string, List<GridMovement>>();
	static Queue<string> turnKey = new Queue<string>();
	static Queue<GridMovement> turnTeam = new Queue<GridMovement>();


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (turnTeam.Count == 0) {
			InitTeamTurnQueue();
		}	
	}

	static void InitTeamTurnQueue () 
	{
		List<GridMovement> teamList = units[turnKey.Peek()];

		foreach (GridMovement unit in teamList) 
		{
			turnTeam.Enqueue(unit);
		}

		StartTurn ();
	}

	public static void StartTurn () 
	{
		if (turnTeam.Count > 0) 
		{
			turnTeam.Peek().BeginTurn();
		}
	}

	public static void EndTurn () 
	{
		GridMovement unit = turnTeam.Dequeue ();
		unit.EndTurn ();

		if (turnTeam.Count > 0) {
			//start next teammeber's turn
			StartTurn ();
		} 
		else 
		{
			//remove team form front of list and add them to the back, then start next team's turn
			string team = turnKey.Dequeue();
			turnKey.Enqueue(team);
			InitTeamTurnQueue();
		}
	}

	public static void AddUnit (GridMovement unit) 
	{
		List<GridMovement> list;

		if (!units.ContainsKey(unit.tag)) 
		{
			list = new List<GridMovement> ();
			units[unit.tag] = list;

			if (!turnKey.Contains(unit.tag)) 
			{
				turnKey.Enqueue(unit.tag);
			}
		} 
		else 
		{
			list = units[unit.tag];
		}

		list.Add(unit);
	}

}
