using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : ProjectileScript {

	public float radius = 10f;
	public GameObject explosionEffect;

	void Start()
	{
		damage = 2f;
	}
		

	public override void TriggerProjectile (Collider other)
	{
		GameObject exp = Instantiate (explosionEffect, transform.position, transform.rotation);
		//exp.AddComponent<ProjectileScript> ();
		ParticleSystem part = explosionEffect.GetComponent<ParticleSystem> ();
		float destTime =  part.main.duration -1 ;
		Destroy (exp,destTime);
		//ParticleSystem expPart = exp.GetComponent<ParticleSystem> ();

		Collider[] coll = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider col in coll) {
			HealthSystem hs = col.gameObject.GetComponent<HealthSystem> ();
			if (hs != null) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, hs.transform.position, out hit)) {
					if(hit.collider.gameObject.tag=="Ship")
						hs.damaged (ship.transform.parent.gameObject, team, damage);
				}
			}
		}
	}
		


}
