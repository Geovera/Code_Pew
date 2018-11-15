using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform redInitPos;
	public Transform blueInitPos;

	List<Player> players;
	List<Enemy> enemies;

	/* Start the game */
	void Awake () {
		players = new List<Player> ();
		players.Add(new Player(this, GameEnums.Team.Blue));
		enemies = new List<Enemy> ();
		players[0].initPlayer (blueInitPos);
		Enemy temp = (new Enemy(this, GameEnums.Team.Red));
		temp.initPlayer (redInitPos);
		enemies.Add (temp);
	
	}
	
	/* Check for player revive(Not final) */
	void LateUpdate () {
		if(Input.GetKeyDown(KeyCode.Space))
			players[0].restartShip();
		
	}

	/* Display any kill event on all the players UI */
	public void displayKillOnUIs(string a, string b)
	{
		foreach (Player player in players) {
			player.displayOnUI (a,b);
		}
	}



	


	
}
