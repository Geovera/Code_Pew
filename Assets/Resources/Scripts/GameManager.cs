using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject redSpawnShip;
	public GameObject blueSpawnShip;
	Transform[] redSpawnPoints;
	Transform[] blueSpawnPoints;

	List<Player> players;
	List<Bot> bots;

	GameObject redTeam;
	GameObject blueTeam;

	int maxTeamSize = 5;
	int redTeamSize;
	int blueTeamSize;

	/* Start the game */
	void Awake () {
		redSpawnPoints = redSpawnShip.GetComponentsInChildren<Transform> ();
		blueSpawnPoints = blueSpawnShip.GetComponentsInChildren<Transform> ();

		redTeam = new GameObject ("RedTeam");
		blueTeam = new GameObject ("BlueTeam");

		redTeamSize = 0;
		blueTeamSize = 0;

		players = new List<Player> ();
		bots = new List<Bot> ();
		initPlayers ();

		initBots ();

	
	}
	
	/* Check for player revive(Not final) */
	void LateUpdate () {
		if(Input.GetKeyDown(KeyCode.Space))
			players[0].restartShip();
		
	}

	void initPlayers()
	{
		int numPlayers = 1;
		for (int i = 0; i < numPlayers; i++) {
			GameEnums.Team t;
			string pla = "Player" + i;
			int team = PlayerPrefs.GetInt (pla);
			Transform pos;
			if (team == 0) {
				t = GameEnums.Team.Blue;
				pos = blueSpawnPoints[blueTeamSize+1];
				blueTeamSize++;
			}
			else {
				t = GameEnums.Team.Red;
				pos = redSpawnPoints[redTeamSize+1];
				redTeamSize++;
			}
			Player plTemp = new Player (this, t);
			players.Add (plTemp);
			plTemp.initPlayer (pos);
			if (team == 0)
				plTemp.player.transform.parent = blueTeam.transform;
			else
				plTemp.player.transform.parent = redTeam.transform;
		}
	}

	void initBots()
	{
		while (redTeamSize < maxTeamSize) {
			Bot red = (new Bot (this, GameEnums.Team.Red));
			bots.Add (red);
			red.initBot (redSpawnPoints[redTeamSize+1]);
			redTeamSize++;
			red.bot.transform.parent = redTeam.transform;
		}
		while (blueTeamSize < maxTeamSize) {
			Bot blue = (new Bot (this, GameEnums.Team.Blue));
			bots.Add (blue);
			blue.initBot (blueSpawnPoints[blueTeamSize+1]);
			blueTeamSize++;
			blue.bot.transform.parent = blueTeam.transform;

		}
	}

	

	/* Display any kill event on all the players UI */
	public void displayKillOnUIs(string a, string b)
	{
		foreach (Player player in players) {
			player.displayOnUI (a,b);
		}
	}



	


	
}
