using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {



	public float countdown;
	bool alive = true;
	public GameEnums.Team team;
	protected float damage = 1.0f;
	public GameObject ship;

	/* Default lifetime of projectile */
	public ProjectileScript()
	{
		countdown = 10f;
	}
		
	void LateUpdate()
	{
		lifeTimeCheck();
	}

	public GameEnums.Team Team
	{
		get{return team;}
		set{ team = value;}
	}
		
	/* Check for any trigger as long as it is not the parent ship or another projectile */
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != ship && other.gameObject.tag!="Projectile") {
			toDestroyObject (other);
		}
	}

	/* Call the damaged function if the collider has a HealthSystem component
	 * Do the damage based on the variable */
	public virtual void TriggerProjectile(Collider other)
	{
		if (other != null) {
			HealthSystem hs = other.GetComponent<HealthSystem> ();
			if (hs != null) {
				hs.damaged (ship.transform.parent.gameObject, team, damage);
			}
		}
	}

	/* Checklifetime of projectile */
	void lifeTimeCheck()
	{
		countdown -= Time.deltaTime;
		if (countdown <= 0.0f && alive)
		{
			toDestroyObject(null);
			alive = false;
		}
	}
		
	/* Try to do damage to collider before destroying itself */
	virtual public void toDestroyObject(Collider other)
	{
		TriggerProjectile (other);
		Destroy (gameObject);
	}
}
