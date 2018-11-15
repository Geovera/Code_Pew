using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {

	/* Template script for specialized ship scripts */

	public GameObject[] abilitiesPreFabs;
	public KeyCode[] abilitiesKeys;

	public float maxHealth;
    public float maxSpeed;
	public GameEnums.Team team = GameEnums.Team.Blue;
	//Explosion effect on death
	public static string explodePath = "Prefabs/Effects/BigExplosionEffect";

	protected Quaternion offset;
	protected ShipComponents shCp;


	public List<Ability> abilities;

	public void changeTeams(GameEnums.Team tm)
	{
		this.team = tm;
	}
}
