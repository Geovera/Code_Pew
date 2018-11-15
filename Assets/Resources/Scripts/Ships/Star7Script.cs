using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Star7Script : ShipScript {

	/* Initializating of ship type Star7 with abilities */

	public float shotSpeed;
	public Transform bombSpawn;


	void Awake () {

		offset = new Quaternion (1f, .005f, 0, 1);	//Rotational offset for projectile fire
		shCp = new ShipComponents (this.gameObject);

		abilities = new List<Ability>();
		// Adding all abilities to ship
		abilities.Add(new TurretAbility(gameObject, abilitiesPreFabs[0], shCp.Turrets,offset,shotSpeed, "Textures/Abilities/LaserIcon", .2f));
		abilities.Add(new ProjectileAbility(gameObject, abilitiesPreFabs[1], bombSpawn,offset, shotSpeed, "Textures/Abilities/BombIcon", 2f));

	}
		

}
