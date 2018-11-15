using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour {

	/* Script to manage abilities for a player
	 * Initiates UI cooldown */

	List<AbilityCDDisplay> cdsDisplay;
	public ShipScript shipAbilities;
	bool isAlive;


	/* Get and Set for life state of ship */
	public bool IsAlive{
		get{ return isAlive;}
		set{isAlive = value;}
	}

	/* Get abilities from ship object and add to Icon display list */
	public void getAbilities(GameObject abi,GameObject abilityIcon, GameObject ship)
	{
		cdsDisplay = new List<AbilityCDDisplay> ();
		shipAbilities = ship.GetComponent<ShipScript>();
		Vector3 offset = abi.transform.position;
		foreach (Ability a in shipAbilities.abilities) {
			GameObject newIcon = GameObject.Instantiate (abilityIcon, abi.transform);
			newIcon.transform.position = offset;
			AbilityCDDisplay cdTemp = newIcon.AddComponent<AbilityCDDisplay> ();
			cdTemp.initialize (a);
			cdsDisplay.Add (cdTemp);
			offset.x += 70;
		}

	}
	/* Only run the update if the ship object is alive */
	void Update () 
	{
		if (isAlive) 
		{
			manageAbilities ();
		}
	
	}

	/* Check for player input and trigger ability accordingly */
	void manageAbilities()
	{
		if (shipAbilities.abilities != null) {
			for (int i = 0; i < shipAbilities.abilitiesKeys.Length; i++) {
				if (Input.GetKeyDown (shipAbilities.abilitiesKeys [i])) {
					cdsDisplay [i].ButtonTriggered ();
					shipAbilities.abilities [i].TriggerAbility ();
				}
			}
		}
	}
}
