using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
	
	/* Delegate to set a destruction sequence on death */
	public delegate void destructionSequence(string a, string b);

	public destructionSequence destroySeq;

	float maxHealth;
	private float health;
	public GameEnums.Team team;
	bool isAlive;
	public GameObject destroyExplosion;
	public float exCd;

	public float MaxHealth
	{
		get{return maxHealth;}
	}

	public float Health
	{
		get{ return health; }
		set{ health = value; }
	}

	void Start()
	{
		health = maxHealth;
		isAlive = true;
	}

	/* Initilizing max health component */
	public void init(float h)
	{
		maxHealth = h;
		health = maxHealth;
		isAlive = true;
	}

	/* Set destruction effect on death */
	public void setDestruction(string path, float cd)
	{
		exCd = cd;
		destroyExplosion = Resources.Load (path) as GameObject;
	}


	/* Call this function from other objects that damage this object */
	public void damaged(GameObject obj, GameEnums.Team tm, float dg)
	{
		if(tm!=team && isAlive)
		{
			health -= dg;
			checkHealth (obj.name);
		}
	}
		
	/* Detect any collisions that occur on this object */
	private void OnCollisionEnter(Collision collision)
	{
		if (isAlive)
		{
			string tagCol = collision.gameObject.tag;
			this.health -= 1.0f;

			
		}
		checkHealth(collision.gameObject.name);
	}

	/* Check current health with attacker as a parameter */
	void checkHealth(string name)
	{
		if (health <= .0f)
		{
			isAlive = false;
			destroyObject(name);

		}
		else
			print("Hit by: " + name + " Health: "+ this.health);
	}

	/* Call the destruction sequence on this object
	 * Initilize the destruction effect
	 * Destroy effect after some it is finished
	 * Destroy this object */
	void destroyObject(string enem)
	{
		if (destroySeq!=null)
			destroySeq(gameObject.transform.parent.name, enem);
		GameObject exp = Instantiate (destroyExplosion, transform.position, transform.rotation);
		ParticleSystem part = destroyExplosion.GetComponent<ParticleSystem> ();
		float destTime = part.main.duration -1f;
		Destroy (exp,destTime);

		DestroyObject(gameObject);
	}

}
