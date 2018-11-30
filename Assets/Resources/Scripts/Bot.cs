using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot  {

	private Object[] ships;
	public GameObject bot;
	GameManager gameManager;
	GameEnums.Team team;
	AI shipControl;

	// Use this for initialization
	public Bot(GameManager man, GameEnums.Team t)
	{
		team = t;
		gameManager = man;
		ships = Resources.LoadAll("Prefabs/Ships");
	}


	GameObject selectShip()
	{
		int index = Random.Range(1,2);
		return (GameObject)ships[index];

	}

	private void initShip()
	{
		GameObject aShip = GameObject.Instantiate (selectShip (), bot.transform);
		aShip.transform.parent = bot.transform;
		aShip.GetComponent<ShipScript> ().changeTeams(this.team);
		if (bot.GetComponent<AI> () == null) {
			shipControl= aShip.AddComponent<AI> ();
			shipControl.init(this.team);
		}
			
		aShip.AddComponent<HealthSystem> ();
		aShip.GetComponent<HealthSystem> ().init (aShip.GetComponent<ShipScript>().maxHealth);
		aShip.GetComponent<HealthSystem> ().setDestruction (ShipScript.explodePath, 6);
		aShip.GetComponent<HealthSystem> ().destroySeq = destroyShip;
		aShip.GetComponent<HealthSystem> ().team = this.team;

	}

	public void restartShip()
	{
		if (!shipControl.IsAlive) {
			initShip ();
		} else {
			GameManager.print ("Already Exists");
		}
	}


	public void initBot(Transform initPos)
	{
		if(bot==null)
		{

			bot = new GameObject ();
			bot.transform.position = initPos.position;
			bot.transform.rotation = initPos.rotation;
			bot.name = "Bot";
			initShip ();
			//if(player.gameObject.GetComponent<ShipManager>()==null)
			//if(player.gameObject.GetComponent<PlayerMovement>()==null)

			//gameManager.plaMethods.Invoke ();
		}
	}

	public void destroyShip(string me, string other)
	{
		shipControl.IsAlive = false;
		gameManager.displayKillOnUIs (other, me);

	}
}
