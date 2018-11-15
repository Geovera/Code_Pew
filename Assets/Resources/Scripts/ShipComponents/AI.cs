using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AI : MonoBehaviour {
	
	/* AI manager component for a fighter ship */
	GameObject targetEnemy;
	public GameEnums.Team team;
	GameEnums.AIState state;
	float detectionRadius;
	Collider[] coll;
	public float fov;
	public Vector3 minDistanceDetect;
	public int dir;



	public ShipComponents shComps;
	public Throttle th;

	public float sharpness;
	public float notSeenTarget;

	float nextDecision;
	public float nextMoveDecision;

	bool isAlive;

	public bool IsAlive
	{
		get{ return isAlive;}
		set{isAlive=value;}
	}

	/* Initilize AI script with team 
	 * Initlize all data to be use in AI calculations */
	public void init(GameEnums.Team t)
	{
		team = t;
		isAlive = true;
		detectionRadius = 100f;
		nextDecision = 1f;
		fov = 60f;
		minDistanceDetect = new Vector3(10,10,10);

		shComps = new ShipComponents (gameObject);
		th = new Throttle (7, 14);
		state = GameEnums.AIState.OnSearch;
		notSeenTarget = 20.0f;
		nextMoveDecision = 2.0f;
	}

	/* Use Physics OverlapSphere to check for any ships within detection radius
	 * Set ship target as long as it is on the opposite team */
	void checkNearbyShips()
	{
		coll = Physics.OverlapSphere (transform.position, detectionRadius);
		foreach (Collider col in coll) {
			if (col.gameObject.tag == "Ship" && col.gameObject!=shComps.Ship) {
				if(col.gameObject.GetComponent<ShipScript>().team!=this.team)
				{
					if(targetEnemy==null)
						targetEnemy = col.gameObject;
					state = GameEnums.AIState.OnPursuit;
				}
			}
			else if(targetEnemy==null){
				state = GameEnums.AIState.OnSearch;
			}

		}
	}

	/* Check current state of ship and act execute function accordingly */
	void checkState()
	{
		switch (state) {
		case GameEnums.AIState.Idle:
			idleFlight ();
			break;
		case GameEnums.AIState.OnPursuit:
			pursuitFlight();
			break;
		case GameEnums.AIState.OnSearch:
			searchFlight ();
			break;
		default:
			break;
		}
	}
	
	/* Check for enemies if the target is null 
	 * Make a movement decision
	 * Check current state of ship
	 * Rotate turrets towards target */
	void FixedUpdate () {
		if (isAlive) {
			if (targetEnemy == null)
				checkNearbyShips ();
			movementDecision ();
			checkState ();
			turretRot ();
		}
		
	}

	/* If the target is not within the field of view, but it is still close to the ship, keep the target on sight */
	bool targetClose()
	{
		if (targetEnemy == null)
			return false;
		float resultant = targetEnemy.transform.position.magnitude - transform.position.magnitude;
		print (resultant);
		if (resultant< 100.0f) {
			notSeenTarget = 0.0f;
			return true;
		}

		return false;
	}

	/* Routine for activily searching for a enemy target 
	 * Randomly rotate the ship based on dir*/
	void searchFlight()
	{
		if (canSeeTarget()){
			state = GameEnums.AIState.OnPursuit;
			return;
		}
		checkNearbyShips ();

		float rotX = 0.0f;
		float rotY = 0.0f;
		switch (dir) {
		case 1:
			rotX += 5f;
			break;
		case 2:
			rotX -= 5f;
			break;
		case 3:
			rotY += 10f;
			break;
		case 4:
			rotY -= 10f;
			break;
		default:
			break;

		}

		shComps.Rb.AddRelativeForce(new Vector3(0, 0.0f, th.Cur) * gameObject.GetComponent<ShipScript>().maxSpeed);

		Vector3 ROT = new Vector3(rotX, 0.0f, rotY);
		Quaternion deltaRotation = Quaternion.Euler(ROT * .05f);
		shComps.Rb.MoveRotation(shComps.Rb.rotation * deltaRotation);

	}

	/* Rest routine for ship */
	void idleFlight(){

	}

	/* Function to decide which attack should be used next(Needs revision) */
	void attackDecision ()
	{
		if (canSeeTarget ()) {
			if (nextDecision < 0.0f) {
				int attackType = Random.Range (0, 100);
				if (attackType > 0 && attackType < 75) {
					shComps.Script.abilities [0].TriggerAbility ();
				} else if (attackType < 100) {
					shComps.Script.abilities [1].TriggerAbility ();
				}
				nextDecision = 1f;

			} else
				nextDecision -= .01f;
		}
	}

	/* Function to decide which direction should the the ship rotate towards(Needs revision) */
	void movementDecision()
	{
		if (notSeenTarget >= 19.0f) {
			if (nextMoveDecision < 0.0f) {
				state = GameEnums.AIState.OnSearch;
				dir = Random.Range (0, 5);
				nextMoveDecision = 2f;
			} else
				nextMoveDecision -= .01f;
		}
	}

	/* Is the target whitin a certain field of view of the ship? */
	bool canSeeTarget(){
		if (targetEnemy == null)
			return false;
		Vector3 rayDirection = targetEnemy.transform.position - transform.position;

		float angle =Vector3.Angle(rayDirection, transform.forward);
		if(angle <= fov){
			notSeenTarget = 0.0f;
			return true;
		}else{
			notSeenTarget += .01f;
			return false;
		}
	}

	/* Flight routine for a ship that has a target in sight(Needs revision) */
	void pursuitFlight()
	{
		float z = th.Cur;

		if (canSeeTarget()) {
			th.Increase ();
			
		} else {
			th.Decrease ();
		}

		Vector3 toTarget = targetEnemy.transform.position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation (toTarget);
		sharpness = .015f - th.Cur*.0006f;
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, sharpness);
		shComps.Rb.AddRelativeForce(new Vector3(0, 0.0f, z) * 10);
		attackDecision ();

	}

	/* Call the rotate function of Ship Components and pass the position of the enemy */
	void turretRot()
	{
		if (targetEnemy != null) {
			shComps.turretRot (targetEnemy.transform.position);
		}

	}


}
