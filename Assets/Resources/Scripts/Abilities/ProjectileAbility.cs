using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : Ability {

	GameObject ship;
	GameObject projectile;
	Transform transform;
	Vector3 pos;
	Quaternion offset;
	float projectileSpeed;


	/*Initilize Projectile ability*/
	public ProjectileAbility(GameObject sh, GameObject proj, Transform tm, Quaternion off, float proSp, string path, float cd)
	{
		aSprite = Resources.Load<Sprite> (path) as Sprite;
		coolDown = new cooldownTimer (cd);
		ship = sh;
		projectile = proj;
		transform = tm;
		offset = off;
		projectileSpeed = proSp;
		if (transform.gameObject.GetComponent<ParticleSystem> () != null)
			partSys = transform.gameObject.GetComponent<ParticleSystem> ();


	}
	/*Call this function when triggering ability from ShipManager
	 * Check for cd
	 * Init projectile prefab based on offset rotation
	 * Play any particle system if the ability has any.
	 * Set team of projectile and add velocity  */
	public override void TriggerAbility()
	{
		if (coolDown.checkCD ()) {
			coolDown.resetCD ();
			pos = transform.TransformPoint (Vector3.forward);

			Quaternion direction = transform.rotation;
			direction *= offset;
			GameObject pro = GameObject.Instantiate (projectile, pos, direction) as GameObject;
			pro.GetComponent<ProjectileScript> ().ship = transform.parent.gameObject;
			if (partSys != null)
				partSys.Play ();
			pro.GetComponent<ProjectileScript> ().setTeam (ship.GetComponent<ShipScript> ().team);
			pro.GetComponent<Rigidbody> ().velocity = transform.forward * (projectileSpeed + ship.GetComponent<Rigidbody> ().velocity.magnitude);
		} else
			Debug.Log ("Ability not ready!");
	}

}
