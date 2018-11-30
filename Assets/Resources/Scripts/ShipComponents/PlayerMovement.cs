using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour {

	public Throttle th;
	public ShipComponents shCmps;

	bool canMove = true;



	Camera mouseCamera;

	/* Initilize components of ship
	 * Get player camera for turret rotation */
	void Start () {
		th = new Throttle (9, 20);
		shCmps = new ShipComponents (gameObject);
	}

	public void setPlayerCam(Camera cam)
	{
		mouseCamera = cam;
	}
		

	/* Rotate turrets 
	 * Move the ship has long as it is possible */
	private void FixedUpdate()
	{
		turretRot();
		if (canMove)
			keyBoardMove();
	}

	/* The ship is always going fowards
	 * Change the throttle of the ship with scroolwheel(Needs revision)
	 * Rotate the ship in the y and x axis to change direction of the ship */
	void keyBoardMove()
	{

		float z =th.Cur;
		float rotX = 0;
		float rotZ = 0;
		float changeSpeed = Input.GetAxis("Mouse ScrollWheel");
		Vector3 localVel = transform.InverseTransformDirection(shCmps.Rb.velocity);

		if(changeSpeed>0)
		{
			th.Increase ();
		}
		else if(changeSpeed<0)
		{
			th.Decrease ();
		}
		if (th.Cur < 0 && localVel.z < 1)
			z = 0;


		if(Input.GetKey(KeyCode.W))
		{

			rotX += 40f;
		}
		if(Input.GetKey(KeyCode.S))
		{
			rotX = -40f;
		}
		if(Input.GetKey(KeyCode.A))
		{
			rotZ = 40f;
		}
		if (Input.GetKey(KeyCode.D))
		{
			rotZ = -40f;
		}
			
		shCmps.Rb.AddRelativeForce(new Vector3(0, 0.0f, z) * shCmps.Speed);

		Vector3 ROT = new Vector3(rotX, 0.0f, rotZ);
		Quaternion deltaRotation = Quaternion.Euler(ROT * .05f);
		shCmps.Rb.MoveRotation(shCmps.Rb.rotation * deltaRotation);
	}

	/* Call the rotate function of Ship Components and pass the position of the mouse in world space  */
	void turretRot()
	{
		Ray mouse = mouseCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(mouse, out hit) && hit.collider.gameObject!=this.gameObject)
		{

			shCmps.turretRot(hit.point);
		}

	}

}
