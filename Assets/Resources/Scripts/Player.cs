using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	private Object[] ships;
	GameObject playerPrefab;
	GameObject abilitiesPrefab;
	public ShipManager shipManager;
	GameObject player;
	GameManager gameManager;
	GameEnums.Team team;
	public GameObject playerUI;
	public GameObject abilityIcon;
	GameObject initUI;

	/*Set team and a reference to the game manager
	 * Load all Prefabs needed to init a ship*/
	public Player(GameManager man, GameEnums.Team t)
	{
		team = t;
		gameManager = man;
		playerUI = Resources.Load ("Prefabs/UI/PlayerUI") as GameObject;
		abilityIcon = Resources.Load ("Prefabs/UI/AbilityIcon") as GameObject;
		playerPrefab = Resources.Load ("Prefabs/Player/Player") as GameObject;
		abilitiesPrefab = Resources.Load ("Prefabs/UI/Abilities") as GameObject;
		ships = Resources.LoadAll("Prefabs/Ships");
	}

	/*Select Ship based on ShipSelection Scene*/
	GameObject selectShip()
	{
		int index = PlayerPrefs.GetInt("ship");
		return (GameObject)ships[index];

	}
	/* Initialize a prefab Ship
	 * Set the team
	 * Add user directed movement
	 * Add a Ship Manager Script
	 * Initialize a Canvas UI with abilities
	 * Added a camera to the ship
	 * Add a health system and added to the UI
	 * */
	private void initShip()
	{
		GameObject aShip = GameObject.Instantiate (selectShip (), player.transform);
		aShip.transform.parent = player.transform;
		aShip.GetComponent<ShipScript> ().changeTeams(this.team);
		aShip.AddComponent<PlayerMovement> ();
		if (player.GetComponent<ShipManager> () == null) {
			player.AddComponent<ShipManager> ();
		}
		shipManager = player.GetComponent<ShipManager> ();
		if (initUI == null) {
			initUI = GameObject.Instantiate (playerUI);
			initUI.name = player.name + "UI";
			GameObject.Instantiate (abilitiesPrefab, initUI.transform);
		}


		GameObject.Destroy (initUI.transform.GetChild (2).gameObject);
		GameObject abi = GameObject.Instantiate (abilitiesPrefab, initUI.transform);

		shipManager.getAbilities(abi, abilityIcon, aShip);
		shipManager.IsAlive = true;
		player.GetComponent<MSCameraController> ().setCamera ();
		player.GetComponent<MSCameraController> ().originalPosition [0].transform.parent = aShip.transform;
		player.GetComponent<MSCameraController> ().temp.transform.parent = aShip.transform;
		aShip.AddComponent<HealthSystem> ();
		aShip.GetComponent<HealthSystem> ().init (shipManager.shipAbilities.maxHealth);
		aShip.GetComponent<HealthSystem> ().setDestruction (ShipScript.explodePath, 6);
		aShip.GetComponent<HealthSystem> ().destroySeq = destroyPlayer;
		aShip.GetComponent<HealthSystem> ().team = this.team;
		initUI.transform.GetChild (1).GetComponent<UIHealthDisplay> ().init (aShip.GetComponent<HealthSystem> ());


	}
	/*Reinstantiate a new ship*/
	public void restartShip()
	{
		if (!shipManager.IsAlive) {
			initShip ();
		} else {
			GameManager.print ("Already Exists");
		}
	}
		
	/*Initialize the player at initPos*/
	public void initPlayer(Transform initPos)
	{
		if(player==null)
		{
			
			player = GameObject.Instantiate(playerPrefab, initPos);
			player.transform.parent = null;
			player.name = "Player";
			initShip ();
			//if(player.gameObject.GetComponent<ShipManager>()==null)
			//if(player.gameObject.GetComponent<PlayerMovement>()==null)

			//gameManager.plaMethods.Invoke ();
		}
	}
	/*Delegate.
	 * Destruction Sequence on ship death*/
	public void destroyPlayer(string me, string other)
	{
		shipManager.IsAlive = false;
		gameManager.displayKillOnUIs (other, me);

	}
	/*Display any UI related stuff onto the player screen*/
	public void displayOnUI(string a, string b)
	{
		EventsControl eControl = initUI.transform.GetChild(0).GetComponent<EventsControl> ();
		if ( eControl!= null) {
			eControl.addEvent (a, "Killed", b);
		} else
			GameManager.print ("No EventControl");
	}



}
