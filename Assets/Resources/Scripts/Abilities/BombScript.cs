using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : ProjectileScript {

	public float radius = 10f;
	public GameObject explosionEffect;

	/* Initilize the script and damage */
	void Start()
	{
		damage = 2f;
	}
		
	/* On lifetime end or collision 
	 * Cause a radius explosion that does damage to objects
	 * Check if there is a shield that nulls the damage in between the explosion and the object */
	public override void TriggerProjectile (Collider other)
	{
		GameObject exp = Instantiate (explosionEffect, transform.position, transform.rotation);
		ParticleSystem part = explosionEffect.GetComponent<ParticleSystem> ();
		float destTime =  part.main.duration -1 ;
		Destroy (exp,destTime);
		Collider[] coll = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider col in coll) {
			HealthSystem hs = col.gameObject.GetComponent<HealthSystem> ();
			if (hs != null) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, hs.transform.position, out hit)) {
					if(hit.collider.gameObject.tag!="Shield")
						hs.damaged (ship.transform.parent.gameObject, team, damage);
				}
			}
		}
	}
		


}
