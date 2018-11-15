using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Star6Script : ShipScript {

	public float shotSpeed;

	// Use this for initialization
	void Awake () {
		offset = new Quaternion (1f, .005f, 0, 1);
		shCp = new ShipComponents (this.gameObject);
		abilities = new List<Ability>();
		abilities.Add(new TurretAbility(gameObject, abilitiesPreFabs[0], shCp.Turrets,offset,shotSpeed, "Textures/Abilities/LaserIcon", .1f));

	}


}
