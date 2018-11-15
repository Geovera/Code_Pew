using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbility : Ability {

	GameObject ship;
	GameObject projectile;
	List<Transform> turrets;
	List<ProjectileAbility> turretFire;
	Quaternion offset;
	float projectileSpeed;

	/*Initialize a turret ability*/
	public TurretAbility(GameObject sh, GameObject proj, List<Transform> tm, Quaternion off, float proSp, string path, float cd)
	{
		coolDown = new cooldownTimer (cd);
		aSprite = Resources.Load<Sprite> (path);

		ship = sh;
		projectile = proj;
		turrets = tm;
		offset = off;
		projectileSpeed = proSp;

		turretFire = new List<ProjectileAbility> ();

		foreach (Transform turret in turrets) 
		{
			turretFire.Add (new ProjectileAbility (ship, projectile, turret, offset, projectileSpeed, path, cd));
		}
		turretFire.RemoveAt (turretFire.Count-1);



	}
	/*Call this function when triggering ability from ShipManager*/
	public override void TriggerAbility()
	{
		if (coolDown.checkCD ()) {
			coolDown.resetCD ();
			foreach (ProjectileAbility fire in turretFire) {
				fire.TriggerAbility ();
			}
		} else
			Debug.Log ("Ability not ready!");
	}

}
