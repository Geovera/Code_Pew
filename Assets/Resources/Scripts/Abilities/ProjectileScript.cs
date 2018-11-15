using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	//public float delay;

	public float countdown;
	bool alive = true;
	public GameEnums.Team team;
	protected float damage = 1.0f;
	public GameObject ship;

	public ProjectileScript()
	{
		countdown = 10f;
	}

	// Update is called once per frame
	void LateUpdate()
	{

		lifeTimeCheck();

	}

	public void setTeam(GameEnums.Team tm)
	{
		team = tm;
	}

	public GameEnums.Team getTeam()
	{
		return team;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != ship) {
			ToDestroyObject (other);
		}
	}
		
	public virtual void TriggerProjectile(Collider other)
	{
		if (other != null) {
			HealthSystem hs = other.GetComponent<HealthSystem> ();
			if (hs != null) {
				hs.damaged (ship.transform.parent.gameObject, team, damage);
			}
		}
	}


	void lifeTimeCheck()
	{
		countdown -= Time.deltaTime;
		if (countdown <= 0.0f && alive)
		{
			ToDestroyObject(null);
			alive = false;
		}
	}
		

	virtual public void ToDestroyObject(Collider other)
	{
		TriggerProjectile (other);
		Destroy (gameObject);
	}
}
