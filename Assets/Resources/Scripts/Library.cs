using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/* Author: Geovanny Vera
 * Library of useful classes for game dev */


/*Throttle class for vehicles*/
public class Throttle
{
	int min;
	int max;
	int cur;

	public int Cur
	{
		get {
			return cur;
		}
	}

	public Throttle()
	{
		min = 1;
		max = 3;
		cur = max - min;
	}

	public Throttle(int mi, int ma)
	{
		min = mi;
		max = ma;
		cur = max - min;
	}
	public Throttle(int mi, int ma, int cu)
	{
		new Throttle(mi, ma);
		cur = cu;
	}

	public void Increase()
	{
		if(cur<max)
			cur++;
	}

	public void Decrease()
	{
		if (cur > min)
			cur--;
	}


}

/*Class for accessing ship components(Code_pew) */
public class ShipComponents
{
	GameObject ship;
	Rigidbody rb;
	ShipScript scrp;
	List<Transform> turr;

	public GameObject Ship
	{
		get{
			return ship;
		}
	}

	public Rigidbody Rb
	{
		get{
			return rb;
		}
	}

	public ShipScript Script
	{
		get{
			return scrp;
		}
	}

	public float Speed
	{
		get{
			return scrp.maxSpeed;
		}
	}

	public List<Transform> Turrets
	{
		get{
			return turr;
		}
	}

	public ShipComponents(GameObject go)
	{
		ship = go;
		rb = ship.GetComponent<Rigidbody> ();
		scrp = ship.GetComponent<ShipScript> ();
		turr = ship.GetComponentsInChildren<Transform>().ToList<Transform>();
		turr.RemoveAll(x => x.gameObject.tag != "Turret");

	}

	public void turretRot(Vector3 pos)
	{
		foreach (Transform turret in turr) {
			var lookRotationPoint = pos - turret.transform.position;
			turret.transform.rotation = Quaternion.LookRotation (lookRotationPoint.normalized);
		}

	}

}

/* Class for any kind of cooldown*/
public class cooldownTimer
{
	float coolDownDuration;
	float curTime;
	float nextReadyTime;
	float coolDownTimeLeft;

	public float TimeCD
	{
		get{ return coolDownDuration;}
	}

	public float CurTime
	{
		get{return curTime;}
	}

	public float getNextReady()
	{
		if (curTime > Time.time) {
			return curTime - Time.time;
		} else
			return 0f;
	}

	public void resetCD()
	{
		curTime = Time.time + coolDownDuration;

	}
	public cooldownTimer(float dur)
	{
		coolDownDuration = dur;
		curTime = Time.time + coolDownDuration;
	}

	public bool checkCD()
	{
		if (curTime < Time.time) {
			return true;
		}
		return false;
	}

}

